using System.Collections.Generic;
using TraktDl.Business.Database.SqLite;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Mock.Database
{
    public class MockDatabase : IDatabase
    {
        public List<ApiKey> ApiKeys { get; set; }

        public List<Show> Shows { get; set; }

        public MockDatabase()
        {
            ApiKeys = new List<ApiKey>();
            Shows = new List<Show>();
        }

        public void AddApiKey(ApiKey apiKey)
        {
            ApiKeys.Add(apiKey);
        }

        public void AddOrUpdateShows(List<ShowSql> shows)
        {
            //Shows.AddRange(shows);
        }

        public List<Show> GetMissingEpisode()
        {
            var res = new List<Show>();

            foreach (var show in Shows)
            {
                foreach (var season in show.Seasons)
                {
                    foreach (var episode in season.Episodes)
                    {
                        if (episode.Status == EpisodeStatus.Missing)
                            res.Add(show);
                    }
                }
            }

            return res;
        }

        public void ClearMissingEpisodes()
        {

        }

        public bool ResetBlacklist()
        {
            return true;
        }
    }
}
