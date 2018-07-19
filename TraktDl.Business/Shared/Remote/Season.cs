using System;
using System.Collections.Generic;
using System.Linq;

namespace TraktDl.Business.Shared.Remote
{
    public class Season
    {
        public Guid Id { get; }

        public Show Show { get; }

        public int SeasonNumber { get; set; }

        public bool Blacklisted { get; set; }

        public List<Episode> MissingEpisodes => Episodes.Where(e => e.Status == EpisodeStatus.Missing).ToList();

        public List<Episode> Episodes { get; set; }

        public Season()
        {
            Episodes = new List<Episode>();
        }

        public Season(Guid id, Show show) : this()
        {
            Id = id;
            Show = show;
        }
    }
}
