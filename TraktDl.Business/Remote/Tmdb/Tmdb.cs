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

            foreach (var showSql in shows)
            {
                tasks.Add(RefreshShow(client, showSql));
            }

            Task.WaitAll(tasks.ToArray());

            Database.AddOrUpdateShows(shows);

            return true;
        }

        private async Task RefreshShow(TMDbClient client, ShowSql showSql)
        {
            List<Task> tasks = new List<Task>();

            // Refresh episodes
            foreach (var seasonSql in showSql.Seasons)
            {
                foreach (var episodeSql in seasonSql.Episodes)
                {
                    tasks.Add(RefreshEpisode(client, episodeSql));
                }
            }


            // Missing show infos
            if (string.IsNullOrEmpty(showSql.PosterUrl))
            {
                TvShow tvShow = await client.GetTvShowAsync(Convert.ToInt32(showSql.Providers[ProviderSql.Tmdb]), TvShowMethods.ExternalIds, "fr-FR").ConfigureAwait(false);

                showSql.Update(client.Config, tvShow);
            }

            // ensure episode update if finished
            Task.WaitAll(tasks.ToArray());
        }

        private async Task RefreshEpisode(TMDbClient client, EpisodeSql episodeSql)
        {
            if (string.IsNullOrEmpty(episodeSql.PosterUrl))
            {
                TvEpisode tvEpisode = await client.GetTvEpisodeAsync(Convert.ToInt32(episodeSql.Season.Show.Providers[ProviderSql.Tmdb]), episodeSql.Season.SeasonNumber, episodeSql.EpisodeNumber, TvEpisodeMethods.ExternalIds, "fr-FR").ConfigureAwait(false);

                episodeSql.Update(client.Config, tvEpisode);
            }
        }

    }
}
