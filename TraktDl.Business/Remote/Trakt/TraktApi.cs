using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using TraktDl.Business.Database.SqLite;
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

        private string ApiKeyName => TraktApiClient.GetMode == ApiMode.Sandbox ? "Trakt.Sandbox" : "Trakt";

        public TraktApi(ITraktApiClient client, IDatabase database)
        {
            TraktApiClient = client;
            Database = database;

            Client = GetClient();
        }

        private TraktClient GetClient()
        {
            TraktClient client = TraktApiClient.Client;

            if (!client.IsValidForAuthenticationProcess)
            {
                throw new InvalidOperationException("Trakt Client not valid for authentication");
            }

            client.Configuration.ForceAuthorization = true;

            return client;
        }

        private void SetupClient()
        {
            if (IsUsable)
            {
                TraktToken token = GetAuthToken();
                TraktAuthorization authorization = TraktAuthorization.CreateWith(token.AccessToken, token.RefreshToken);
                Client.Authorization = authorization;

                RefreshAuthorization().Wait();
            }
            else
            {
                throw new Exception("Not authenticated");
            }
        }

        private async Task TryToDeviceAuthenticate()
        {
            TraktResponse<ITraktAuthorization> authorization = await Client.Authentication.PollForAuthorizationAsync();

            if (authorization.HasValue && authorization.Value.IsValid)
            {
                SaveAuthToken(new TraktToken { AccessToken = authorization.Value.AccessToken, RefreshToken = authorization.Value.RefreshToken });

                Console.WriteLine("-------------- Authentication successful --------------");
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
            Database.AddApiKey(new ApiKeySql { Id = ApiKeyName, ApiData = JsonConvert.SerializeObject(token) });
        }

        private TraktToken GetAuthToken()
        {
            ApiKeySql key = Database.GetApiKey(ApiKeyName);

            if (key != null)
            {
                return JsonConvert.DeserializeObject<TraktToken>(key.ApiData);
            }

            return null;
        }

        public bool IsUsable => GetAuthToken() != null;

        public async Task<DeviceToken> GetDeviceToken()
        {
            TraktResponse<ITraktDevice> device = await Client.Authentication.GenerateDeviceAsync().ConfigureAwait(false);

            if (device.HasValue && device.Value.IsValid)
            {
                Console.WriteLine("You have to authenticate this application.");
                Console.WriteLine($"Please visit the following webpage: {device.Value.VerificationUrl}");
                Console.WriteLine($"Sign in or sign up on that webpage and enter the following code: {device.Value.UserCode}");

                await TryToDeviceAuthenticate();

                return new DeviceToken { Token = device.Value.UserCode, Url = device.Value.VerificationUrl };
            }

            return null;
        }

        public async Task<bool> CheckAuthent(string deviceToken)
        {
            TraktResponse<ITraktAuthorization> authorization = await Client.Authentication.GetAuthorizationAsync(deviceToken);

            if (authorization.HasValue && authorization.Value.IsValid)
            {
                SaveAuthToken(new TraktToken { AccessToken = authorization.Value.AccessToken, RefreshToken = authorization.Value.RefreshToken });

                return true;
            }

            return false;
        }

        private void RefreshHiddenItem()
        {
            Task<List<ShowSql>> showRefresh = RefreshShowHiddenItem();
            showRefresh.Wait();

            Database.AddOrUpdateShows(showRefresh.Result);

            Task<List<ShowSql>> seasonRefresh = RefreshSeasonHiddenItem(showRefresh.Result);
            seasonRefresh.Wait();

            Database.AddOrUpdateShows(seasonRefresh.Result);
        }

        private async Task<List<ShowSql>> RefreshShowHiddenItem()
        {
            List<ShowSql> res = new List<ShowSql>();
            TraktPagedResponse<ITraktUserHiddenItem> hiddenShow = await Client.Users
                .GetHiddenItemsAsync(TraktHiddenItemsSection.ProgressCollected, TraktHiddenItemType.Show)
                .ConfigureAwait(false);

            TraktPagedResponse<ITraktUserHiddenItem> hiddenShowRes = hiddenShow;

            foreach (ITraktUserHiddenItem traktUserHiddenItem in hiddenShowRes)
            {
                ShowSql localShow = GetShow(traktUserHiddenItem.Show.Ids.Trakt);

                localShow.Update(traktUserHiddenItem);

                res.Add(localShow);
            }

            return res;
        }

        private async Task<List<ShowSql>> RefreshSeasonHiddenItem(List<ShowSql> shows)
        {
            Dictionary<uint, ShowSql> res = shows.ToDictionary(s => s.Id);
            TraktPagedResponse<ITraktUserHiddenItem> hiddenSeason = await Client.Users.GetHiddenItemsAsync(TraktHiddenItemsSection.ProgressCollected, TraktHiddenItemType.Season).ConfigureAwait(false);

            TraktPagedResponse<ITraktUserHiddenItem> hiddenSeasonRes = hiddenSeason;

            foreach (ITraktUserHiddenItem traktUserHiddenItem in hiddenSeasonRes)
            {
                ShowSql localShow = res.ContainsKey(traktUserHiddenItem.Show.Ids.Trakt) ? res[traktUserHiddenItem.Show.Ids.Trakt] : GetShow(traktUserHiddenItem.Show.Ids.Trakt);

                SeasonSql localSeason = GetSeason(localShow, traktUserHiddenItem.Season.Number.Value);

                localSeason.Update(traktUserHiddenItem);

                res[localShow.Id] = localShow;
            }

            return res.Values.ToList();
        }

        //private readonly object _getShowLock = new object();
        private readonly object _getShowLock = new object();

        private object ShowLock()
        {
            return _getShowLock;
        }

        private ShowSql GetShow(uint id)
        {
            ShowSql localShow = null;

            lock (ShowLock())
            {
                localShow = Database.GetShow(id);

                if (localShow == null)
                {
                    localShow = new ShowSql(true)
                    {
                        Id = id,
                    };
                }
            }

            return localShow;
        }

        private SeasonSql GetSeason(ShowSql show, int seasonNumber)
        {
            SeasonSql localSeason = null;

            lock (ShowLock())
            {
                localSeason = show.Seasons.SingleOrDefault(s => s.SeasonNumber == seasonNumber);

                if (localSeason == null)
                {
                    localSeason = new SeasonSql(show)
                    {
                        SeasonNumber = seasonNumber,
                    };
                    show.Seasons.Add(localSeason);
                }
            }

            return localSeason;
        }

        private EpisodeSql GetEpisode(SeasonSql season, int episodeNumber)
        {
            EpisodeSql localEpisode = null;

            lock (ShowLock())
            {
                localEpisode = season.Episodes.SingleOrDefault(s => s.EpisodeNumber == episodeNumber);

                if (localEpisode == null)
                {
                    localEpisode = new EpisodeSql(season)
                    {
                        EpisodeNumber = episodeNumber,
                        Status = EpisodeStatusSql.Unknown,
                    };
                    season.Episodes.Add(localEpisode);
                }
            }

            return localEpisode;
        }


        public bool RefreshMissingEpisodes()
        {
            SetupClient();

            RefreshHiddenItem();

            // Set all missing to unknown
            Database.ClearMissingEpisodes();

            Task<TraktListResponse<ITraktCollectionShow>> collected = Client.Users.GetCollectionShowsAsync("me", new TraktExtendedInfo { Full = true });
            Task<TraktListResponse<ITraktWatchedShow>> watched = Client.Users.GetWatchedShowsAsync("me", new TraktExtendedInfo { Full = true });

            List<TraktShow> shows = new List<TraktShow>();

            watched.Wait();
            TraktListResponse<ITraktWatchedShow> watchedRes = watched.Result;

            foreach (ITraktWatchedShow traktWatchedShow in watchedRes)
            {
                int watchedEpisodes = traktWatchedShow.WatchedSeasons.Where(s => s.Number != 0).Sum(season => season.Episodes.Count());

                TraktShow show = new TraktShow
                {
                    Id = traktWatchedShow.Ids.Trakt,
                    Year = traktWatchedShow.Year,
                    SerieName = traktWatchedShow.Title,
                    Watched = watchedEpisodes >= traktWatchedShow.AiredEpisodes,
                    Status = traktWatchedShow.Status,
                    Imdb = traktWatchedShow.Ids.Imdb,
                    Tmdb = traktWatchedShow.Ids.Tmdb,
                };

                foreach (ITraktWatchedShowSeason season in traktWatchedShow.WatchedSeasons)
                {
                    TraktSeason traktSeason = new TraktSeason { Season = season.Number.Value };

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
                TraktShow show = shows.SingleOrDefault(s => s.Id == traktCollectionShow.Ids.Trakt);
                if (show == null)
                {
                    show = new TraktShow
                    {
                        Id = traktCollectionShow.Ids.Trakt,
                        Year = traktCollectionShow.Year,
                        SerieName = traktCollectionShow.Title,
                        Status = traktCollectionShow.Status,
                        Imdb = traktCollectionShow.Ids.Imdb,
                        Tmdb = traktCollectionShow.Ids.Tmdb,
                    };

                    shows.Add(show);
                }

                foreach (ITraktCollectionShowSeason season in traktCollectionShow.CollectionSeasons)
                {
                    TraktSeason misSeason = show.Seasons.SingleOrDefault(e => e.Season == season.Number);
                    if (misSeason == null)
                    {
                        misSeason = new TraktSeason { Season = season.Number.Value };
                        show.Seasons.Add(misSeason);
                    }

                    foreach (ITraktCollectionShowEpisode episode in season.Episodes)
                    {
                        TraktEpisode misEpisode = misSeason.MissingEpisodes.SingleOrDefault(e => e.Episode == episode.Number);
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
            List<ShowSql> bddShows = Database.GetShows();
            List<ShowSql> updateShows = new List<ShowSql>();

            // Remove Show blacklist
            shows.RemoveAll(s => bddShows.Any(b => b.Id == s.Id && b.Blacklisted));
            List<Task> tasks = new List<Task>();

            foreach (TraktShow traktShow in shows)
            {
                ShowSql localShow = GetShow(traktShow.Id);
                localShow.Update(traktShow);
                updateShows.Add(localShow);
                tasks.Add(HandleProgress(traktShow, localShow));
            }

            Task.WaitAll(tasks.ToArray());

            Database.AddOrUpdateShows(updateShows);

            Database.ClearUnknownEpisodes();

            shows.RemoveAll(s => !s.Seasons.Any());

            return true;
        }

        private async Task HandleProgress(TraktShow traktShow, ShowSql bddShow)
        {
            // Remove Season blacklist
            traktShow.Seasons.RemoveAll(s => bddShow.Seasons.Any(b => b.SeasonNumber == s.Season && b.Blacklisted));

            if (traktShow.Seasons.Any())
            {
                TraktResponse<TraktApiSharp.Objects.Get.Shows.ITraktShowCollectionProgress> collectionProgress =
                    await Client.Shows.GetShowCollectionProgressAsync(traktShow.Id.ToString(), false, false, false).ConfigureAwait(false);

                TraktResponse<TraktApiSharp.Objects.Get.Shows.ITraktShowCollectionProgress> collectionProgressRes = collectionProgress;

                foreach (ITraktSeasonCollectionProgress season in collectionProgressRes.Value.Seasons)
                {
                    TraktSeason misSeason = traktShow.Seasons.SingleOrDefault(e => e.Season == season.Number);
                    if (misSeason == null)
                    {
                        misSeason = new TraktSeason { Season = season.Number.Value };
                        traktShow.Seasons.Add(misSeason);
                    }

                    foreach (ITraktEpisodeCollectionProgress episode in season.Episodes)
                    {
                        // Already existing in missing
                        if (misSeason.MissingEpisodes.All(m => m.Episode != episode.Number))
                        {
                            misSeason.MissingEpisodes.Add(new TraktEpisode { Episode = episode.Number.Value, Collected = episode.Completed.HasValue && episode.Completed.Value });
                        }
                    }
                }

                foreach (TraktSeason showSeason in traktShow.Seasons)
                {
                    SeasonSql season = GetSeason(bddShow, showSeason.Season);

                    // BlackList Ended series if complete
                    if ((traktShow.Status == TraktShowStatus.Ended || traktShow.Status == TraktShowStatus.Canceled) && !showSeason.MissingEpisodes.Any())
                    {
                        season.Blacklisted = true;
                    }

                    if (!season.Blacklisted)
                    {
                        // Save missing episode
                        foreach (TraktEpisode missingEpisode in showSeason.MissingEpisodes)
                        {
                            EpisodeSql ep = GetEpisode(season, missingEpisode.Episode);
                            ep.Status = missingEpisode.Collected || missingEpisode.Watched ? EpisodeStatusSql.Collected : EpisodeStatusSql.Missing;
                        }
                    }

                    // Remove watched from missing
                    showSeason.MissingEpisodes.RemoveAll(m => m.Watched);

                    // Remove collected from missing
                    showSeason.MissingEpisodes.RemoveAll(m => m.Collected);
                }

                traktShow.Seasons.RemoveAll(s => !s.MissingEpisodes.Any());
            }
        }
    }
}
