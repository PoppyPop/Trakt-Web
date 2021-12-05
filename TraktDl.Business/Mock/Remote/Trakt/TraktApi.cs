using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraktDl.Business.Database.SqLite;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Mock.Remote.Trakt
{
    public class TraktApi : ITrackingApi
    {
        public string GetMode => "Mock";

        public bool IsUsable(IDatabase database) => true;

        public TraktApi()
        {

        }

        public bool RefreshMissingEpisodes(IDatabase database)
        {
            List<ShowSql> result = new List<ShowSql>();

            var show = new ShowSql(true)
            {
                Id = 99718,
                Name = "Westworld",
                Providers = new Dictionary<ProviderSql, string> { { ProviderSql.Imdb, "tt0475784" }, { ProviderSql.Tmdb, "63247" } },
            };

            var season = new SeasonSql()
            {
                SeasonNumber = 2,
            };
            season.Episodes = new List<EpisodeSql>
            {
                new EpisodeSql() {EpisodeNumber = 3, Status = EpisodeStatusSql.Missing},
                new EpisodeSql() {EpisodeNumber = 4, Status = EpisodeStatusSql.Missing},
                new EpisodeSql() {EpisodeNumber = 5, Status = EpisodeStatusSql.Collected}
            };

            show.Seasons = new List<SeasonSql> { season };

            result.Add(show);

            result.Add(new ShowSql(true)
            {
                Id = 123,
                Name = "z fake",
            });

            result.Add(new ShowSql(true)
            {
                Id = 456,
                Name = "9 fake",
            });

            database.AddOrUpdateShows(result);

            return true;
        }

#pragma warning disable 1998
        public async Task<DeviceToken> GetDeviceToken(IDatabase database) => new DeviceToken();

        public async Task<bool> CheckAuthent(IDatabase database, string deviceToken) => true;

#pragma warning restore 1998
    }
}
