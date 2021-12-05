using System.Collections.Generic;
using Newtonsoft.Json;
using TraktDl.Business.Shared.Database;

namespace TraktDl.Business.Database.SqLite
{
    public class ShowSql
    {
        public virtual uint Id { get; set; }

        public virtual bool Blacklisted { get; set; }

        public virtual string Name { get; set; }

        public virtual int? Year { get; set; }

        public virtual string PosterUrl { get; set; }

        public virtual IList<SeasonSql> Seasons { get; set; }

        public virtual string ProvidersData
        {
            get => JsonConvert.SerializeObject(Providers);
            set
            {
                Providers = new Dictionary<ProviderSql, string>();
                if (!string.IsNullOrEmpty(value))
                    Providers = JsonConvert.DeserializeObject<Dictionary<ProviderSql, string>>(value);
            }
        }

        public virtual Dictionary<ProviderSql, string> Providers { get; set; }

        public ShowSql()
        {
            Providers = new Dictionary<ProviderSql, string>();
            Seasons = new List<SeasonSql>();
        }

        public ShowSql(bool create) : this()
        {
            
        }


        
    }
}
