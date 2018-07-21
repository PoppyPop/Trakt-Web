﻿using System;
using System.Collections.Generic;
using TraktDl.Business.Database.SqLite;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Mock.Remote.Trakt
{
    public class TraktApi : ITrackingApi
    {
        public string GetMode => "Mock";

        private IDatabase Database { get; }

        public TraktApi(IDatabase database)
        {
            Database = database;
        }

        public bool RefreshMissingEpisodes()
        {
            List<ShowSql> result = new List<ShowSql>();

            var show = new ShowSql
            {
                Id = 99718,
                Name = "Westworld",
                Providers = new Dictionary<string, string> { { "Imdb", "tt0475784" }, { "Tmdb", "63247" } },
            };

            var season = new SeasonSql()
            {
                SeasonNumber = 2,
            };
            season.Episodes = new List<EpisodeSql>
            {
                new EpisodeSql() {EpisodeNumber = 3, Status = EpisodeStatusSqLite.Missing},
                new EpisodeSql() {EpisodeNumber = 4, Status = EpisodeStatusSqLite.Missing},
                new EpisodeSql() {EpisodeNumber = 5, Status = EpisodeStatusSqLite.Collected}
            };

            show.Seasons = new List<SeasonSql> { season };

            result.Add(show);

            result.Add(new ShowSql
            {
                Id = 123,
                Name = "z fake",
            });

            result.Add(new ShowSql
            {
                Id = 456,
                Name = "9 fake",
            });

            Database.AddOrUpdateShows(result);

            return true;
        }
    }
}
