using System;
using System.Collections.Generic;
using System.Linq;

namespace TraktDl.Business.Shared.Remote
{
    public class Season
    {
        public Guid Id { get; }

        public int SeasonNumber { get; set; }

        public bool Blacklisted { get; set; }

        public List<Episode> Episodes { get; set; }

        public decimal Percent
        {
            get
            {
                var total = Episodes.Count;

                if (total == 0)
                    return 100;

                var nbMissings = Episodes.Count(e => e.Status == EpisodeStatus.Missing);

                return 100 - (nbMissings * 100 / total);
            }
        }

        public Season()
        {
            Episodes = new List<Episode>();
        }

        public Season(Guid id) : this()
        {
            Id = id;
        }
    }
}
