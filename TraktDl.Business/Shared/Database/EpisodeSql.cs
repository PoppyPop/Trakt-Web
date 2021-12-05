using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TraktDl.Business.Shared.Database;

namespace TraktDl.Business.Database.SqLite
{
    public class EpisodeSql
    {
        public virtual Guid Id { get; set; }

        public virtual SeasonSql Season { get; set; }

        public virtual int EpisodeNumber { get; set; }

        public virtual EpisodeStatusSql Status { get; set; }

        public virtual string Name { get; set; }

        public virtual string PosterUrl { get; set; }

        public virtual DateTime? AirDate { get; set; }

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

        public EpisodeSql()
        {
            Providers = new Dictionary<ProviderSql, string>();
        }

        public EpisodeSql(SeasonSql season) : this()
        {
            Season = season;
        }
    }
}
