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

        public Dictionary<Provider, string> Providers { get; set; }

        public List<Season> Seasons { get; set; }

        public decimal Percent => Math.Round(Seasons.Average(s => s.Percent));

        public string PosterUrl { get; set; }

        public Show()
        {
            Providers = new Dictionary<Provider, string>();
            Seasons = new List<Season>();
            Blacklisted = false;
        }
    }
}
