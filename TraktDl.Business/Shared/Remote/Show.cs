using System;
using System.Collections.Generic;
using System.Linq;

namespace TraktDl.Business.Shared.Remote
{
    public class Show
    {
        public uint Id { get; set; }

        public string SerieName { get; set; }

        public int? Year { get; set; }

        public bool Blacklisted { get; set; }

        //public Dictionary<Provider, string> Providers { get; set; }

        public List<Season> Seasons { get; set; }

        public decimal Percent => Math.Round(Seasons.Average(s => s.Percent));

        public string PosterUrl { get; set; }

        public int TotalEpisodes => Seasons.Sum(s => s.TotalEpisodes);

        public int CollectedEpisodes => Seasons.Sum(s => s.CollectedEpisodes);

        public int MissingEpisodes => Seasons.Sum(s => s.MissingEpisodes);

        public Episode NextEpisodeToCollect => Seasons.OrderBy(s => s.SeasonNumber).FirstOrDefault(s => s.NextEpisodeToCollect != null)?.NextEpisodeToCollect;

        public Show()
        {
            //Providers = new Dictionary<Provider, string>();
            Seasons = new List<Season>();
            Blacklisted = false;
        }
    }
}
