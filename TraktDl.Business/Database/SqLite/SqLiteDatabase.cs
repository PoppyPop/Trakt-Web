using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
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

        public List<Show> GetMissingEpisode()
        {
            var res = context.Shows
                .Join(context.Seasons, show => show.Id, season => season.ShowID, (show, season) => new { show, season })
                .Where(s => s.season.Episodes.Any(ep => ep.Status == EpisodeStatusSqLite.Missing))
                .Select(s => s.show).ToList();


            return res.Select(s => s.Convert()).ToList();
        }


        public void ClearMissingEpisodes()
        {
            var episodes = context.Episodes.Where(e => e.Status == EpisodeStatusSqLite.Missing);
            foreach (var episodeSqLite in episodes)
            {
                episodeSqLite.Status = EpisodeStatusSqLite.Unknown;
            }

            context.SaveChanges();
        }
    }
}
