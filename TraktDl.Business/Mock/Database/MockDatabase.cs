using System.Collections.Generic;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Mock.Database
{
    public class MockDatabase : IDatabase
    {
        public List<BlackListShow> BlackLists { get; set; }

        public List<ApiKey> ApiKeys { get; set; }

        public List<Show> Shows { get; set; }

        public MockDatabase()
        {
            BlackLists = new List<BlackListShow>();
            ApiKeys = new List<ApiKey>();
            Shows = new List<Show>();
        }

        public void AddApiKey(ApiKey apiKey)
        {
            ApiKeys.Add(apiKey);
        }

        public void AddOrUpdateShows(List<Show> shows)
        {
            Shows.AddRange(shows);
        }

        public List<Episode> GetMissingEpisode()
        {
            var res = new List<Episode>();

            foreach (var show in Shows)
            {
                foreach (var season in show.Seasons)
                {
                    foreach (var episode in season.Episodes)
                    {
                        if (episode.Status == EpisodeStatus.Missing)
                            res.Add(episode);
                    }
                }
            }

            return res;
        }

        public void ClearMissingEpisodes()
        {

        }

        public void AddBlackList(BlackListShow blackListShow)
        {
            BlackLists.Add(blackListShow);
        }
    }
}
