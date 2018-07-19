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

        private string ConfigPath => Path.Combine(Directory.GetCurrentDirectory(), Client.Configuration.UseSandboxEnvironment ? "Sandbox" : "", "trakt.json");

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
            Directory.CreateDirectory(Path.GetDirectoryName(ConfigPath));
            File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(token));
        }

        private TraktToken GetAuthToken()
        {
            if (File.Exists(ConfigPath))
            {
                return JsonConvert.DeserializeObject<TraktToken>(File.ReadAllText(ConfigPath));
            }

            return null;
        }

        private async Task RefreshHiddenItem()
        {
            var hiddenShow = await Client.Users.GetHiddenItemsAsync(TraktHiddenItemsSection.ProgressCollected, TraktHiddenItemType.Show).ConfigureAwait(false);

            TraktPagedResponse<ITraktUserHiddenItem> watchedRes = hiddenShow;

            foreach (ITraktUserHiddenItem traktUserHiddenItem in watchedRes)
            {
                var traktShow = traktUserHiddenItem.Show;

                // Store hidden season
                var show = new BlackListShow
                {
                    TraktShowId = traktShow.Ids.Trakt,
                    Entire = true,
                    SerieName = traktShow.Title
                };
                Database.AddBlackList(show);
            }
        }

        public List<Show> GetMissingEpisodes()
        {
            RefreshHiddenItem().Wait();

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
                    var traktSeason = new TraktSeason { Season = season.Number };

                    foreach (ITraktWatchedShowEpisode episode in season.Episodes)
                    {
                        traktSeason.MissingEpisodes.Add(new MissingEpisode { Episode = episode.Number, Watched = true });
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
                        misSeason = new TraktSeason { Season = season.Number };
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
                            misSeason.MissingEpisodes.Add(new MissingEpisode { Episode = episode.Number, Collected = true });
                        }
                    }
                }
            }

            shows.RemoveAll(s => s.Watched);

            // Prepare blacklist
            List<BlackListShow> blackList = Database.BlackLists;

            // Remove Show blacklist
            shows.RemoveAll(s => blackList.Any(b => b.TraktShowId == s.Id && b.Entire));
            List<Task> tasks = new List<Task>();

            foreach (TraktShow traktShow in shows)
            {
                tasks.Add(HandleProgress(traktShow, blackList));
            }

            Task.WaitAll(tasks.ToArray());

            shows.RemoveAll(s => !s.Seasons.Any());

            return shows.Select(s => (Show)s).ToList();
        }

        private async Task HandleProgress(TraktShow traktShow, List<BlackListShow> blackList)
        {
            // Remove Season blacklist
            traktShow.Seasons.RemoveAll(s => blackList.Any(b => b.TraktShowId == traktShow.Id && b.Season == s.Season));

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
                        misSeason = new TraktSeason { Season = season.Number };
                        traktShow.Seasons.Add(misSeason);
                    }

                    foreach (ITraktEpisodeCollectionProgress episode in season.Episodes.Where(e =>
                        !e.Completed.HasValue || !e.Completed.Value))
                    {
                        // Already existing in missing
                        if (misSeason.MissingEpisodes.All(m => m.Episode != episode.Number))
                        {
                            misSeason.MissingEpisodes.Add(new MissingEpisode { Episode = episode.Number });
                        }
                    }
                }

                foreach (ITraktSeason season in collectionProgressRes.Value.HiddenSeasons)
                {
                    TraktSeason traktSeason = traktShow.Seasons.SingleOrDefault(e => e.Season == season.Number);
                    if (traktSeason != null)
                    {
                        // Store hidden season
                        var show = new BlackListShow
                        {
                            TraktShowId = traktShow.Id,
                            Season = season.Number,
                            SerieName = traktShow.SerieName
                        };
                        Database.AddBlackList(show);

                        traktSeason.Hidden = true;
                    }
                }

                foreach (var showSeason in traktShow.Seasons)
                {
                    // Remove watched from missing
                    showSeason.MissingEpisodes.RemoveAll(m => m.Watched);

                    // Remove collected from missing
                    showSeason.MissingEpisodes.RemoveAll(m => m.Collected);

                    // BlackList Ended series if complete
                    if ((traktShow.Status == TraktShowStatus.Ended || traktShow.Status == TraktShowStatus.Canceled) && !showSeason.MissingEpisodes.Any())
                    {
                        // Store hidden season
                        var show = new BlackListShow
                        {
                            TraktShowId = traktShow.Id,
                            Season = showSeason.Season,
                            SerieName = traktShow.SerieName
                        };
                        Database.AddBlackList(show);
                    }
                }

                traktShow.Seasons.RemoveAll(s => s.Hidden);

                traktShow.Seasons.RemoveAll(s => !s.MissingEpisodes.Any());
            }
        }
    }
}
