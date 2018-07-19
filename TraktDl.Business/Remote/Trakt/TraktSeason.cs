using System;
using System.Collections.Generic;
using System.Linq;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Remote.Trakt
{
    public class TraktSeason
    {
        public int Season { get; set; }

        public List<TraktEpisode> MissingEpisodes { get; set; }

        public TraktSeason()
        {
            MissingEpisodes = new List<TraktEpisode>();
        }
    }
}
