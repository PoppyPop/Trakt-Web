using System.Collections.Generic;
using System.Linq;
using TraktApiSharp.Enums;
using TraktDl.Business.Shared.Remote;

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

        public Dictionary<string, string> Providers { get; set; }

        public List<TraktSeason> Seasons { get; set; }

        public TraktShow()
        {
            Providers = new Dictionary<string, string>();
            Seasons = new List<TraktSeason>();
        }

        public static implicit operator Show(TraktShow trakt)
        {
            var show = new Show()
            {
                Id = trakt.Id,
                Providers = trakt.Providers,
                SerieName = trakt.SerieName,
                Year = trakt.Year,
                Seasons = trakt.Seasons.Select(s => (Season)s).ToList()
            };

            return show;
        }
    }
}
