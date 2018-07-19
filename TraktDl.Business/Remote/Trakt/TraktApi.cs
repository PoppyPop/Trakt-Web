using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TraktApiSharp;
using TraktApiSharp.Enums;
using TraktApiSharp.Objects.Authentication;
using TraktApiSharp.Objects.Get.Collections;
using TraktApiSharp.Objects.Get.Episodes;
using TraktApiSharp.Objects.Get.Seasons;
using TraktApiSharp.Objects.Get.Users;
using TraktApiSharp.Objects.Get.Watched;
using TraktApiSharp.Requests.Parameters;
using TraktApiSharp.Responses;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Remote.Trakt
{
    public class TraktApi : ITrackingApi
    {
        public string GetMode => "Remote / " + TraktApiClient.GetMode;

        private ITraktApiClient TraktApiClient { get; }

        private IDatabase Database { get; }

        private TraktClient Client { get; set; }

        private string ApiKeyName => Client.Configuration.UseSandboxEnvironment ? "Trakt.Sandbox" : "Trakt";

        public TraktApi(ITraktApiClient client, IDatabase database)
        {
            TraktApiClient = client;
            Database = database;
            SetupClient();
        }

        private void SetupClient()
        {
            var client = TraktApiClient.Client;

            if (!client.IsValidForAuthenticationProcess)
                throw new InvalidOperationException("Trakt Client not valid for authentication");

            client.Configuration.ForceAuthorization = true;

            Client = client;

            var token = GetAuthToken();

            if (token != null)
            {
                var authorization = TraktAuthorization.CreateWith(token.AccessToken, token.RefreshToken);
                client.Authorization = authorization;

                RefreshAuthorization().Wait();
            }
            else
            {
                TryToDeviceAuthenticate().Wait();
            }
        }

        private async Task TryToDeviceAuthenticate()
        {
            TraktResponse<ITraktDevice> device = await Client.Authentication.GenerateDeviceAsync();

            if (device.HasValue && device.Value.IsValid)
            {
                Console.WriteLine("You have to authenticate this application.");
                Console.WriteLine($"Please visit the following webpage: {device.Value.VerificationUrl}");
                Console.WriteLine($"Sign in or sign up on that webpage and enter the following code: {device.Value.UserCode}");

                TraktResponse<ITraktAuthorization> authorization = await Client.Authentication.PollForAuthorizationAsync();

                if (authorization.HasValue && authorization.Value.IsValid)
                {
                    SaveAuthToken(new TraktToken { AccessToken = authorization.Value.AccessToken, RefreshToken = authorization.Value.RefreshToken });

                    Console.WriteLine("-------------- Authentication successful --------------");
                }
            }
        }

        private async Task RefreshAuthorization()
        {
            TraktResponse<ITraktAuthorization> newAuthorization = await Client.Authentication.RefreshAuthorizationAsync();

            if (newAuthorization.HasValue && newAuthorization.Value.IsValid)
            {
                SaveAuthToken(new TraktToken { AccessToken = newAuthorization.Value.AccessToken, RefreshToken = newAuthorization.Value.RefreshToken });
                Console.WriteLine("-------------- Authorization refreshed successfully --------------");
            }
        }

        private void SaveAuthToken(TraktToken token)
        {
            Database.AddApiKey(new ApiKey { Id = ApiKeyName, ApiData = JsonConvert.SerializeObject(token) });
        }

        private TraktToken GetAuthToken()
        {
            var key = Database.ApiKeys.SingleOrDefault(k => k.Id == ApiKeyName);

            if (key != null)
            {
                return JsonConvert.DeserializeObject<TraktToken>(key.ApiData);
            }

            return null;
        }

        private void RefreshHiddenItem()
        {
            var showRefresh = RefreshShowHiddenItem();

            var seasonRefresh = RefreshSeasonHiddenItem();

            Task.WaitAll(showRefresh, seasonRefresh);
        }

        private async Task RefreshShowHiddenItem()
        {
            var hiddenShow = await Client.Users
                .GetHiddenItemsAsync(TraktHiddenItemsSection.ProgressCollected, TraktHiddenItemType.Show)
                .ConfigureAwait(false);

            TraktPagedResponse<ITraktUserHiddenItem> hiddenShowRes = hiddenShow;

            foreach (ITraktUserHiddenItem traktUserHiddenItem in hiddenShowRes)
            {
                var localShow = GetShow(traktUserHiddenItem);

                localShow.Blacklisted = true;

                // Store hidden show
                Database.AddOrUpdateShows(new List<Show> { localShow });
            }
        }

        private async Task RefreshSeasonHiddenItem()
        {
            var hiddenSeason = await Client.Users.GetHiddenItemsAsync(TraktHiddenItemsSection.ProgressCollected, TraktHiddenItemType.Season).ConfigureAwait(false);

            TraktPagedResponse<ITraktUserHiddenItem> hiddenSeasonRes = hiddenSeason;

            foreach (ITraktUserHiddenItem traktUserHiddenItem in hiddenSeasonRes)
            {
                var localShow = GetShow(traktUserHiddenItem);

                var localSeason = GetSeason(localShow, traktUserHiddenItem.Season.Number.Value);

                localSeason.Blacklisted = true;

                // Store hidden show
                Database.AddOrUpdateShows(new List<Show> { localShow });
            }
        }

        private Show GetShow(ITraktUserHiddenItem hiddenItem)
        {
            var traktShow = hiddenItem.Show;

            var localShow = Database.Shows.SingleOrDefault(s => s.Id == traktShow.Ids.Trakt);

            if (localShow == null)
            {
                localShow = new Show
                {
                    Id = traktShow.Ids.Trakt,
                    Blacklisted = true,
                    SerieName = traktShow.Title
                };
            }

            return localShow;
        }

        private Season GetSeason(Show show, int seasonNumber)
        {
            var localSeason = show.Seasons.SingleOrDefault(s => s.SeasonNumber == seasonNumber);

            if (localSeason == null)
            {
                localSeason = new Season(Guid.NewGuid(), show)
                {
                    Blacklisted = true,
                    SeasonNumber = seasonNumber,
                };
            }

            return localSeason;
        }

        private Episode GetEpisode(Season season, int episodeNumber)
        {
            var localEpisode = season.Episodes.SingleOrDefault(s => s.EpisodeNumber == episodeNumber);

            if (localEpisode == null)
            {
                localEpisode = new Episode(Guid.NewGuid(), season)
                {
                    EpisodeNumber = episodeNumber,
                    Status = EpisodeStatus.Unknown,
                };
            }

            return localEpisode;
        }


        public bool RefreshMissingEpisodes()
        {
            RefreshHiddenItem();

            // Set all missing to unknown
            Database.ClearMissingEpisodes();

            var collected = Client.Users.GetCollectionShowsAsync("me", new TraktExtendedInfo { Full = true });
            var watched = Client.Users.GetWatchedShowsAsync("me", new TraktExtendedInfo { Full = true });

            List<TraktShow> shows = new List<TraktShow>();

            watched.Wait();
            TraktListResponse<ITraktWatchedShow> watchedRes = watched.Result;

            foreach (ITraktWatchedShow traktWatchedShow in watchedRes)
            {
                int watchedEpisodes = traktWatchedShow.WatchedSeasons.Sum(season => season.Episodes.Count());

                var show = new TraktShow
                {
                    Id = traktWatchedShow.Ids.Trakt,
                    Year = traktWatchedShow.Year,
                    SerieName = traktWatchedShow.Title,
                    Watched = watchedEpisodes >= traktWatchedShow.AiredEpisodes,
                    Status = traktWatchedShow.Status,
                    Providers = new Dictionary<string, string>
                    {
                        {"Imdb", traktWatchedShow.Ids.Imdb},
                        {"Tmdb", traktWatchedShow.Ids.Tmdb.ToString()}
                    }
                };

                foreach (ITraktWatchedShowSeason season in traktWatchedShow.WatchedSeasons)
                {
                    var traktSeason = new TraktSeason { Season = season.Number.Value };

                    foreach (ITraktWatchedShowEpisode episode in season.Episodes)
                    {
                        traktSeason.MissingEpisodes.Add(new TraktEpisode { Episode = episode.Number.Value, Watched = true });
                    }

                    show.Seasons.Add(traktSeason);
                }

                shows.Add(show);
            }

            collected.Wait();
            TraktListResponse<ITraktCollectionShow> collectedRes = collected.Result;

            foreach (ITraktCollectionShow traktCollectionShow in collectedRes)
            {
                var show = shows.SingleOrDefault(s => s.Id == traktCollectionShow.Ids.Trakt);
                if (show == null)
                {
                    show = new TraktShow
                    {
                        Id = traktCollectionShow.Ids.Trakt,
                        Year = traktCollectionShow.Year,
                        SerieName = traktCollectionShow.Title,
                        Status = traktCollectionShow.Status,
                        Providers = new Dictionary<string, string>
                        {
                            {"Imdb", traktCollectionShow.Ids.Imdb},
                            {"Tmdb", traktCollectionShow.Ids.Tmdb.ToString()}
                        }
                    };

                    shows.Add(show);
                }

                foreach (ITraktCollectionShowSeason season in traktCollectionShow.CollectionSeasons)
                {
                    var misSeason = show.Seasons.SingleOrDefault(e => e.Season == season.Number);
                    if (misSeason == null)
                    {
                        misSeason = new TraktSeason { Season = season.Number.Value };
                        show.Seasons.Add(misSeason);
                    }

                    foreach (ITraktCollectionShowEpisode episode in season.Episodes)
                    {
                        var misEpisode = misSeason.MissingEpisodes.SingleOrDefault(e => e.Episode == episode.Number);
                        if (misEpisode != null)
                        {
                            misEpisode.Collected = true;
                        }
                        else
                        {
                            misSeason.MissingEpisodes.Add(new TraktEpisode { Episode = episode.Number.Value, Collected = true });
                        }
                    }
                }
            }

            shows.RemoveAll(s => s.Watched);

            // PrepareDB 
            List<Show> bddShows = Database.Shows;
            List<Show> updateShows = new List<Show>();

            // Remove Show blacklist
            shows.RemoveAll(s => bddShows.Any(b => b.Id == s.Id && b.Blacklisted));
            List<Task> tasks = new List<Task>();

            foreach (TraktShow traktShow in shows)
            {
                var localShow = bddShows.SingleOrDefault(s => s.Id == traktShow.Id);
                updateShows.Add(localShow);
                tasks.Add(HandleProgress(traktShow, localShow));
            }

            Task.WaitAll(tasks.ToArray());

            Database.AddOrUpdateShows(updateShows);

            shows.RemoveAll(s => !s.Seasons.Any());

            return true;
        }

        private async Task HandleProgress(TraktShow traktShow, Show bddShow)
        {
            // Remove Season blacklist
            traktShow.Seasons.RemoveAll(s => bddShow.Seasons.Any(b => b.SeasonNumber == s.Season && b.Blacklisted));

            if (traktShow.Seasons.Any())
            {
                var collectionProgress =
                    await Client.Shows.GetShowCollectionProgressAsync(traktShow.Id.ToString(), false, false, false).ConfigureAwait(false);

                var collectionProgressRes = collectionProgress;

                foreach (ITraktSeasonCollectionProgress season in collectionProgressRes.Value.Seasons)
                {
                    var misSeason = traktShow.Seasons.SingleOrDefault(e => e.Season == season.Number);
                    if (misSeason == null)
                    {
                        misSeason = new TraktSeason { Season = season.Number.Value };
                        traktShow.Seasons.Add(misSeason);
                    }

                    foreach (ITraktEpisodeCollectionProgress episode in season.Episodes.Where(e =>
                        !e.Completed.HasValue || !e.Completed.Value))
                    {
                        // Already existing in missing
                        if (misSeason.MissingEpisodes.All(m => m.Episode != episode.Number))
                        {
                            misSeason.MissingEpisodes.Add(new TraktEpisode { Episode = episode.Number.Value });
                        }
                    }
                }

                foreach (var showSeason in traktShow.Seasons)
                {
                    var season = GetSeason(bddShow, showSeason.Season);

                    // Remove watched from missing
                    showSeason.MissingEpisodes.RemoveAll(m => m.Watched);

                    // Remove collected from missing
                    showSeason.MissingEpisodes.RemoveAll(m => m.Collected);

                    // BlackList Ended series if complete
                    if ((traktShow.Status == TraktShowStatus.Ended || traktShow.Status == TraktShowStatus.Canceled) && !showSeason.MissingEpisodes.Any())
                    {
                        season.Blacklisted = true;
                    }

                    // Save missing episode
                    foreach (TraktEpisode missingEpisode in showSeason.MissingEpisodes)
                    {
                        var ep = GetEpisode(season, missingEpisode.Episode);
                        ep.Status = EpisodeStatus.Missing;
                    }
                }

                traktShow.Seasons.RemoveAll(s => !s.MissingEpisodes.Any());
            }
        }
    }
}
