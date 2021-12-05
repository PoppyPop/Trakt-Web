using System.Collections.Generic;
using TraktNet.Enums;

namespace TraktDl.Business.Remote.Trakt
{
    public class TraktShow
    {
        public uint Id { get; set; }

        public string SerieName { get; set; }

        public int? Year { get; set; }

        public bool Watched { get; set; }

        public bool CollectionProgress { get; set; }

        public TraktShowStatus Status { get; set; }

        public uint? Tmdb { get; set; }
        public string Imdb { get; set; }

        public List<TraktSeason> Seasons { get; set; }

        public TraktShow()
        {
            Seasons = new List<TraktSeason>();
        }
    }
}
