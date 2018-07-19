using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Database.SqLite
{
    public class SqLiteDatabase : IDatabase
    {
        private SqLiteContext context { get; }

        public SqLiteDatabase()
        {
            context = new SqLiteContext();

            context.Database.Migrate();
        }

        public List<BlackListShow> BlackLists
        {
            get { return context.BlackListShows.Select(b => b.Convert()).ToList(); }
        }

        public List<ApiKey> ApiKeys => context.ApiKeys.Select(b => b.Convert()).ToList();

        public List<Show> Shows => context.Shows.Select(b => b.Convert()).ToList();

        public void AddApiKey(ApiKey apiKey)
        {
            var exist = context.ApiKeys.SingleOrDefault(b => b.Id == apiKey.Id);

            if (exist == null)
            {
                context.ApiKeys.Add(new ApiKeySqLite(apiKey));
            }
            else
            {
                exist.ApiData = apiKey.ApiData;
            }

            context.SaveChanges();
        }

        public void AddOrUpdateShows(List<Show> shows)
        {
            foreach (var show in shows)
            {
                var bddShow = context.Shows.SingleOrDefault(b => b.Id == show.Id);

                if (bddShow == null)
                {
                    context.Shows.Add(new ShowSqLite(show));

                }
                else
                {
                    bddShow.Update(show);
                }
            }

            context.SaveChanges();
        }

        public List<Episode> GetMissingEpisode() => context.Episodes.Where(e => e.Status == EpisodeStatusSqLite.Missing).Select(e => e.Convert())
            .ToList();

        public void ClearMissingEpisodes()
        {
            var episodes = context.Episodes.Where(e => e.Status == EpisodeStatusSqLite.Missing);
            foreach (var episodeSqLite in episodes)
            {
                episodeSqLite.Status = EpisodeStatusSqLite.Unknown;
            }

            context.SaveChanges();
        }

        public void AddBlackList(BlackListShow blackListShow)
        {
            var exist = context.BlackListShows.Any(b => b.TraktShowId == blackListShow.TraktShowId && b.Season == blackListShow.Season && b.Entire == blackListShow.Entire);

            if (!exist)
            {
                context.BlackListShows.Add(new BlackListShowSqLite(blackListShow));
                context.SaveChanges();
            }
        }

        public void RemoveBlackList(BlackListShow blackListShow)
        {
            var toRemove = context.BlackListShows.SingleOrDefault(b => b.Id == blackListShow.Id);

            if (toRemove != null)
            {
                context.BlackListShows.Remove(toRemove);
                context.SaveChanges();
            }
        }

        public void ClearBlackList()
        {
            context.BlackListShows.ToList().ForEach(x => context.BlackListShows.Remove(x));
            context.SaveChanges();
        }
    }
}
