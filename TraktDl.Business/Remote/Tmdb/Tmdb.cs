using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.TvShows;
using TraktDl.Business.Database.SqLite;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Remote.Tmdb
{
    public class Tmdb : IImageApi
    {
        private string ApiKeyName => "Tmdb";

        public Tmdb()
        {

        }

        private TMDbClient Setup(IDatabase database)
        {
            if (IsUsable(database))
            {
                var token = GetAuthToken(database);
                TMDbClient client = new TMDbClient(token, useSsl: true);

                client.GetConfigAsync().Wait();

                return client;
            }

            throw new Exception("Not authenticated");
        }

        public bool RefreshImages(IDatabase database)
        {
            var shows = database.GetMissingImages();

            var client = Setup(database);

            foreach (var showSql in shows)
            {
                RefreshShow(client, showSql).Wait();
            }

            database.AddOrUpdateShows(shows);

            return true;
        }

        public Show RefreshImage(IDatabase database, uint id)
        {
            var show = database.GetShow(id);

            var client = Setup(database);

            RefreshShow(client, show).Wait();

            database.AddOrUpdateShows(new List<ShowSql> { show });
            return show.Convert();
        }


        private string GetAuthToken(IDatabase database)
        {
            var key = database.GetApiKey(ApiKeyName);

            if (key != null)
            {
                return key.ApiData;
            }

            return null;
        }

        public bool IsUsable(IDatabase database) => GetAuthToken(database) != null;

        public Task<DeviceToken> GetDeviceToken(IDatabase database)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckAuthent(IDatabase database, string deviceToken)
        {
            database.AddApiKey(new ApiKeySql() { Id = ApiKeyName, ApiData = deviceToken });

            return true;
        }

        private async Task RefreshShow(TMDbClient client, ShowSql showSql)
        {
            if (!showSql.Blacklisted && showSql.Providers.ContainsKey(ProviderSql.Tmdb) && !string.IsNullOrEmpty(showSql.Providers[ProviderSql.Tmdb]) && int.TryParse(showSql.Providers[ProviderSql.Tmdb], out var showId))
            {
                bool first = true;

                // Refresh episodes
                foreach (var seasonSql in showSql.Seasons.Where(s => !s.Blacklisted))
                {
                    foreach (var episodeSql in seasonSql.Episodes)
                    {
                        if (string.IsNullOrEmpty(episodeSql.PosterUrl) && episodeSql.Status == EpisodeStatusSql.Missing)
                        {
                            if (first)
                            {
                                first = false;
                            }
                            else
                            {
                                await Task.Delay(1000);
                            }


                            await RefreshEpisode(client, episodeSql, showId);
                        }
                    }
                }

                // Missing show infos
                if (string.IsNullOrEmpty(showSql.PosterUrl))
                {
                    TvShow tvShow = await client.GetTvShowAsync(showId, TvShowMethods.ExternalIds, "fr-FR").ConfigureAwait(false);

                    showSql.Update(client.Config, tvShow);
                }
            }
        }

        private async Task RefreshEpisode(TMDbClient client, EpisodeSql episodeSql, int showId)
        {
            TvEpisode tvEpisode = await client.GetTvEpisodeAsync(showId, episodeSql.Season.SeasonNumber, episodeSql.EpisodeNumber, TvEpisodeMethods.ExternalIds, "fr-FR").ConfigureAwait(false);

            if (tvEpisode != null)
                episodeSql.Update(client.Config, tvEpisode);
        }

    }
}
