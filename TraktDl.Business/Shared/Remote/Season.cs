using System.Collections.Generic;

namespace TraktDl.Business.Shared.Remote
{
    public class Season
    {
        public int? SeasonNumber { get; set; }

        public List<Episode> MissingEpisodes { get; set; }

        public Season()
        {
            MissingEpisodes = new List<Episode>();
        }
    }
}
