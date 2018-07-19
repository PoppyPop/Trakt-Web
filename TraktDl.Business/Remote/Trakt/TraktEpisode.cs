using System.Collections.Generic;

namespace TraktDl.Business.Remote.Trakt
{
    public class TraktEpisode
    {
        public int Episode { get; set; }

        public bool Watched { get; set; }

        public bool Collected { get; set; }
    }
}
