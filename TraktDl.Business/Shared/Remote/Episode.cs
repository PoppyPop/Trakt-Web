using System;
using System.Collections.Generic;
using System.Text;

namespace TraktDl.Business.Shared.Remote
{
    public class Episode
    {
        public int EpisodeNumber { get; set; }

        public string Name { get; set; }

        public Dictionary<string, string> Providers { get; set; }

        public Episode()
        {
            Providers = new Dictionary<string, string>();
        }
    }
}
