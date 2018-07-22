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

        public void AddApiKey(ApiKeySql apiKey)
        {
            var exist = context.ApiKeys.SingleOrDefault(b => b.Id == apiKey.Id);

            if (exist == null)
            {
                context.ApiKeys.Add(apiKey);
            }
            else
            {
                exist.ApiData = apiKey.ApiData;
            }

            context.SaveChanges();
        }

        public ApiKeySql GetApiKey(string name)
        {
            return context.ApiKeys.SingleOrDefault(k => k.Id == name);
        }

        public void AddOrUpdateShows(List<ShowSql> shows)
        {
            foreach (var show in shows)
            {
                var bddShow = context.Shows.SingleOrDefault(b => b.Id == show.Id);

                if (bddShow == null)
                {
                    context.Shows.Add(show);
                }
            }

            context.SaveChanges();
        }

        public List<ShowSql> GetShows()
        {
            return context.Shows.ToList();
        }

        public ShowSql GetShow(uint id)
        {
            return context.Shows.SingleOrDefault(s => s.Id == id);
        }

        public List<ShowSql> GetMissingEpisode()
        {
            return context.Shows
                .Join(context.Seasons, show => show.Id, season => season.ShowID, (show, season) => new { show, season })
                .Where(s => s.season.Episodes.Any(ep => ep.Status == EpisodeStatusSql.Missing))
                .Select(s => s.show).ToList();
        }

        public List<ShowSql> GetMissingImages()
        {
            return context.Shows
                .Join(context.Seasons, show => show.Id, season => season.ShowID, (show, season) => new { show, season })
                .Where(s => string.IsNullOrEmpty(s.show.PosterUrl) || s.season.Episodes.Any(ep => string.IsNullOrEmpty(ep.PosterUrl)))
                .Select(s => s.show).ToList();
        }


        public void ClearMissingEpisodes()
        {
            var episodes = context.Episodes.Where(e => e.Status == EpisodeStatusSql.Missing);
            foreach (var episodeSqLite in episodes)
            {
                episodeSqLite.Status = EpisodeStatusSql.Unknown;
            }

            context.SaveChanges();
        }

        public bool ResetBlacklist()
        {
            var blacklistedShow = context.Shows.Where(s => s.Blacklisted);
            foreach (var showSqLite in blacklistedShow)
            {
                showSqLite.Blacklisted = false;
            }

            var blacklistedSeason = context.Seasons.Where(s => s.Blacklisted);
            foreach (var seasonSqLite in blacklistedSeason)
            {
                seasonSqLite.Blacklisted = false;
            }

            context.SaveChanges();

            return true;
        }

        public bool ResetImages()
        {
            var imagesShow = context.Shows.Where(s => !string.IsNullOrEmpty(s.PosterUrl));
            foreach (var showSqLite in imagesShow)
            {
                showSqLite.PosterUrl = null;
            }

            var imagesEpisodes = context.Episodes.Where(s => !string.IsNullOrEmpty(s.PosterUrl));
            foreach (var epîsodeSqLite in imagesEpisodes)
            {
                epîsodeSqLite.PosterUrl = null;
            }

            context.SaveChanges();

            return true;
        }
    }
}
