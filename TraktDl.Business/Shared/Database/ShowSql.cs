using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Database.SqLite
{
    public class ShowSql
    {
        public uint Id { get; set; }

        public virtual bool Blacklisted { get; set; }

        public virtual string Name { get; set; }

        public virtual int? Year { get; set; }

        public virtual string PosterUrl { get; set; }

        public virtual List<SeasonSql> Seasons { get; set; }


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

        public Dictionary<ProviderSql, string> Providers { get; set; }

        public ShowSql()
        {
            
            Providers = new Dictionary<ProviderSql, string>();
        }

        public ShowSql(bool create) : this()
        {
            Seasons = new List<SeasonSql>();
        }


        
    }
}
