using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Database.SqLite
{
    public class EpisodeSql
    {
        public virtual Guid Id { get; set; }

        public virtual Guid SeasonID { get; set; }
        public virtual SeasonSql Season { get; set; }

        public virtual int EpisodeNumber { get; set; }

        public virtual EpisodeStatusSqLite Status { get; set; }

        public virtual string Name { get; set; }

        public virtual string PosterUrl { get; set; }

        public virtual string ProvidersData
        {
            get => JsonConvert.SerializeObject(Providers);
            set
            {
                Providers = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(value))
                    Providers = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
            }
        }

        public Dictionary<string, string> Providers { get; set; }

        public EpisodeSql()
        {
            Providers = new Dictionary<string, string>();
        }

        public EpisodeSql(SeasonSql season) : this()
        {
            Season = season;
        }

        public void Update(Shared.Remote.Episode episode)
        {
            EpisodeNumber = episode.EpisodeNumber;
            Providers = episode.Providers;
            Status = (EpisodeStatusSqLite)Enum.Parse(typeof(EpisodeStatusSqLite),
                Enum.GetName(typeof(EpisodeStatus), episode.Status));
            Name = episode.Name;
            PosterUrl = episode.PosterUrl;
        }

        public Shared.Remote.Episode Convert()
        {
            Shared.Remote.Episode episode = new Shared.Remote.Episode(Id)
            {
                EpisodeNumber = EpisodeNumber,
                Providers = Providers,
                PosterUrl = PosterUrl,
                Status = (EpisodeStatus)Enum.Parse(typeof(EpisodeStatus),
                    Enum.GetName(typeof(EpisodeStatusSqLite), Status))
            };

            return episode;
        }
    }

    public enum EpisodeStatusSqLite
    {
        Unknown,
        Collected,
        Missing
    }

}
