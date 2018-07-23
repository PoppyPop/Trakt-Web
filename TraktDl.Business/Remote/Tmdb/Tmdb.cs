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
        private IDatabase Database { get; }

        public Tmdb(IDatabase database)
        {
            Database = database;
        }

        private TMDbClient Setup()
        {
            TMDbClient client = new TMDbClient("8ba7498e29bcfba224bcf8161d734158");

            client.GetConfigAsync().Wait();

            return client;
        }

        public bool RefreshImages()
        {
            var shows = Database.GetMissingImages();

            var client = Setup();

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

        public Show RefreshImage(uint id)
        {
            var show = Database.GetShow(id);

            var client = Setup();

            RefreshShow(client, show).Wait();

            Database.AddOrUpdateShows(new List<ShowSql> { show });

            return show.Convert();
        }

        private async Task RefreshShow(TMDbClient client, ShowSql showSql)
        {
            if (!showSql.Blacklisted && showSql.Providers.ContainsKey(ProviderSql.Tmdb) && !string.IsNullOrEmpty(showSql.Providers[ProviderSql.Tmdb]) && int.TryParse(showSql.Providers[ProviderSql.Tmdb], out var showId))
            {
                List<Task> tasks = new List<Task>();
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


                            tasks.Add(RefreshEpisode(client, episodeSql, showId));
                        }
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
            TvEpisode tvEpisode = await client.GetTvEpisodeAsync(showId, episodeSql.Season.SeasonNumber, episodeSql.EpisodeNumber, TvEpisodeMethods.ExternalIds, "fr-FR").ConfigureAwait(false);

            episodeSql.Update(client.Config, tvEpisode);
        }

    }
}
