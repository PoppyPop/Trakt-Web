using System.Collections.Generic;
using System.Linq;
using TraktDl.Business.Database.SqLite;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Mock.Database
{
    public class MockDatabase : IDatabase
    {
        private List<ApiKeySql> ApiKeys { get; set; }

        private List<ShowSql> Shows { get; set; }

        public MockDatabase()
        {
            ApiKeys = new List<ApiKeySql>();
            Shows = new List<ShowSql>();
        }

        public void AddApiKey(ApiKeySql apiKey)
        {
            ApiKeys.Add(apiKey);
        }

        public ApiKeySql GetApiKey(string name)
        {
            return ApiKeys.SingleOrDefault(a => a.Id == name);
        }

        public void AddOrUpdateShows(List<ShowSql> shows)
        {
            //Shows.AddRange(shows);
        }

        public List<ShowSql> GetShows()
        {
            return Shows;
        }

        public ShowSql GetShow(uint id)
        {
            return Shows.SingleOrDefault(s => s.Id == id);
        }

        public List<ShowSql> GetMissingEpisode()
        {
            var res = new List<ShowSql>();

            foreach (var show in Shows)
            {
                foreach (var season in show.Seasons)
                {
                    foreach (var episode in season.Episodes)
                    {
                        if (episode.Status == EpisodeStatusSql.Missing)
                            res.Add(show);
                    }
                }
            }

            return res;
        }

        public List<ShowSql> GetMissingImages()
        {
            var res = new List<ShowSql>();

            foreach (var show in Shows)
            {
                if (string.IsNullOrEmpty(show.PosterUrl))
                {
                    res.Add(show);
                }
                else
                {
                    foreach (var season in show.Seasons)
                    {
                        foreach (var episode in season.Episodes)
                        {
                            if (string.IsNullOrEmpty(episode.PosterUrl))
                                res.Add(show);
                        }
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

        public bool ResetImages()
        {
            return true;
        }
    }
}
