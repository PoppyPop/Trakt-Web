using System;
using System.Collections.Generic;
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
        private IDatabase Database { get; }

        public Tmdb(IDatabase database)
        {
            Database = database;
        }

        public bool RefreshImages()
        {
            var shows = Database.GetMissingImages();

            TMDbClient client = new TMDbClient("8ba7498e29bcfba224bcf8161d734158");

            client.GetConfigAsync().Wait();

            List<Task> tasks = new List<Task>();
            int counter = 0;


            foreach (var showSql in shows)
            {
                if (counter % 4 == 0)
                {
                    Task.Delay(1000).Wait();
                }

                tasks.Add(RefreshShow(client, showSql));
                counter++;
            }

            Task.WaitAll(tasks.ToArray());

            Database.AddOrUpdateShows(shows);

            return true;
        }

        private async Task RefreshShow(TMDbClient client, ShowSql showSql)
        {
            int showId;

            if (showSql.Providers.ContainsKey(ProviderSql.Tmdb) && !string.IsNullOrEmpty(showSql.Providers[ProviderSql.Tmdb]) && Int32.TryParse(showSql.Providers[ProviderSql.Tmdb], out showId))
            {
                List<Task> tasks = new List<Task>();

                // Refresh episodes
                foreach (var seasonSql in showSql.Seasons)
                {
                    foreach (var episodeSql in seasonSql.Episodes)
                    {
                        await Task.Delay(1000);

                        tasks.Add(RefreshEpisode(client, episodeSql, showId));
                    }
                }

                // Missing show infos
                if (string.IsNullOrEmpty(showSql.PosterUrl))
                {
                    TvShow tvShow = await client.GetTvShowAsync(showId, TvShowMethods.ExternalIds, "fr-FR").ConfigureAwait(false);

                    showSql.Update(client.Config, tvShow);
                }

                // ensure episode update if finished
                Task.WaitAll(tasks.ToArray());
            }
        }

        private async Task RefreshEpisode(TMDbClient client, EpisodeSql episodeSql, int showId)
        {
            if (string.IsNullOrEmpty(episodeSql.PosterUrl) && episodeSql.Status == EpisodeStatusSql.Missing)
            {
                TvEpisode tvEpisode = await client.GetTvEpisodeAsync(showId, episodeSql.Season.SeasonNumber, episodeSql.EpisodeNumber, TvEpisodeMethods.ExternalIds, "fr-FR").ConfigureAwait(false);

                episodeSql.Update(client.Config, tvEpisode);
            }
        }

    }
}
