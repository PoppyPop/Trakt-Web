using System.Collections.Generic;

namespace TraktDl.Business.Shared.Remote
{
    public class Show
    {
        public uint Id { get; set; }

        public string SerieName { get; set; }

        public int? Year { get; set; }

        public bool Blacklisted { get; set; }

        public Dictionary<string, string> Providers { get; set; }

        public List<Season> Seasons { get; set; }

        public Show()
        {
            Providers = new Dictionary<string, string>();
            Seasons = new List<Season>();
            Blacklisted = false;
        }
    }
}
