using System;
using System.Collections.Generic;
using System.Text;

namespace TraktDl.Business.Shared.Remote
{
    public class Episode
    {
        public Guid Id { get; }

        public int EpisodeNumber { get; set; }

        public int SeasonNumber { get; set; }

        public string Name { get; set; }

        public EpisodeStatus Status { get; set; }

        public Dictionary<Provider, string> Providers { get; set; }

        public string PosterUrl { get; set; }

        public string AirDate { get; set; }

        public Episode()
        {
            Providers = new Dictionary<Provider, string>();
        }

        public Episode(Guid id) : this()
        {
            Id = id;
        }
    }
}
