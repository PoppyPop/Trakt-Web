using System.Collections.Generic;

namespace TraktDl.Business.Shared.Remote
{
    public class Season
    {
        public int? SeasonNumber { get; set; }

        public List<int> MissingEpisodes { get; set; }

        public Season()
        {
            MissingEpisodes = new List<int>();
        }
    }
}
