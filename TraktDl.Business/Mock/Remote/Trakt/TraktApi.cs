using System;
using System.Collections.Generic;
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
            List<Show> result = new List<Show>();

            var show = new Show
            {
                Id = 99718,
                SerieName = "Westworld",
                Providers = new Dictionary<string, string> { { "Imdb", "tt0475784" }, { "Tmdb", "63247" } },
            };

            Season season = new Season(Guid.NewGuid())
            {
                SeasonNumber = 2,
            };
            season.Episodes = new List<Episode>
            {
                new Episode(Guid.NewGuid()) {EpisodeNumber = 3, Status = EpisodeStatus.Missing},
                new Episode(Guid.NewGuid()) {EpisodeNumber = 4, Status = EpisodeStatus.Missing},
                new Episode(Guid.NewGuid()) {EpisodeNumber = 5, Status = EpisodeStatus.Collected}
            };

            show.Seasons = new List<Season> { season };

            result.Add(show);

            result.Add(new Show
            {
                Id = 123,
                SerieName = "z fake",
            });

            result.Add(new Show
            {
                Id = 456,
                SerieName = "9 fake",
            });

            Database.AddOrUpdateShows(result);

            return true;
        }
    }
}
