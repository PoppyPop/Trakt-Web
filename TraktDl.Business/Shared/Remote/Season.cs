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

        public decimal Percent => (TotalEpisodes == 0) ? 100 : (CollectedEpisodes * 100 / TotalEpisodes);

        public int TotalEpisodes => Episodes.Count;

        public int CollectedEpisodes => TotalEpisodes - MissingEpisodes;

        public int MissingEpisodes => Episodes.Count(e => e.Status == EpisodeStatus.Missing);

        public bool HaveMissingEpisodes => MissingEpisodes > 0;

        public Episode NextEpisodeToCollect => Episodes.OrderBy(s => s.EpisodeNumber).FirstOrDefault(s => s.Status == EpisodeStatus.Missing);

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
