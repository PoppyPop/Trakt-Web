using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Database.SqLite
{
    public class EpisodeSql
    {
        public virtual Guid Id { get; set; }

        public virtual Guid SeasonID { get; set; }
        public virtual SeasonSql Season { get; set; }

        public virtual int EpisodeNumber { get; set; }

        public virtual EpisodeStatusSql Status { get; set; }

        public virtual string Name { get; set; }

        public virtual string PosterUrl { get; set; }

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

        public EpisodeSql()
        {
            Providers = new Dictionary<ProviderSql, string>();
        }

        public EpisodeSql(SeasonSql season) : this()
        {
            Season = season;
        }

        public Shared.Remote.Episode Convert()
        {
            Shared.Remote.Episode episode = new Shared.Remote.Episode(Id)
            {
                EpisodeNumber = EpisodeNumber,
                Providers = Providers.ToDictionary(
                    pair => (Provider)Enum.Parse(typeof(Provider), Enum.GetName(typeof(ProviderSql), pair.Key)), 
                    pair => pair.Value),
                PosterUrl = PosterUrl,
                Status = (EpisodeStatus)Enum.Parse(typeof(EpisodeStatus),
                    Enum.GetName(typeof(EpisodeStatusSql), Status)),
                SeasonNumber = Season.SeasonNumber
                    
            };

            return episode;
        }
    }
}
