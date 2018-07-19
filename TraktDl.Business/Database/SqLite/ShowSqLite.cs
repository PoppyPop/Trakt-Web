using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Database.SqLite
{
    public class ShowSqLite
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public uint Id { get; set; }

        [Required]
        public virtual bool Blacklisted { get; set; }

        public virtual string Name { get; set; }

        public virtual int? Year { get; set; }

        public virtual string PosterUrl { get; set; }

        public ICollection<SeasonSqLite> Seasons { get; set; }


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

        /// <summary>
        /// Some cool description
        /// </summary>
        [NotMapped]
        public Dictionary<string, string> Providers { get; set; }


        public ShowSqLite()
        {
            Seasons = new List<SeasonSqLite>();
        }

        public ShowSqLite(Shared.Remote.Show show) : this()
        {
            Id = show.Id;
            Update(show);
        }

        public void Update(Shared.Remote.Show show)
        {
            Name = show.SerieName;
            Providers = show.Providers;
            Blacklisted = show.Blacklisted;
            Year = show.Year;

            foreach (Season showSeason in show.Seasons)
            {
                var bddSeason = Seasons.SingleOrDefault(s => s.SeasonNumber == showSeason.SeasonNumber);
                if (bddSeason == null)
                {
                    Seasons.Add(new SeasonSqLite(showSeason, this));
                }
                else
                {
                    bddSeason.Update(showSeason);
                }
            }
        }

        public Shared.Remote.Show Convert()
        {
            Shared.Remote.Show show = new Shared.Remote.Show
            {
                Id = Id,
                Blacklisted = Blacklisted,
                SerieName = Name,
                Year = Year,
                Seasons = Seasons.Select(s => s.Convert()).ToList(),
                Providers = Providers,
            };

            return show;
        }


    }
    public class SeasonSqLite
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [ForeignKey("Show")]
        public virtual uint ShowID { get; set; }
        public virtual ShowSqLite Show { get; set; }

        public virtual int SeasonNumber { get; set; }

        public virtual ICollection<EpisodeSqLite> Episodes { get; set; }

        [Required]
        public virtual bool Blacklisted { get; set; }


        public SeasonSqLite()
        {
            Episodes = new List<EpisodeSqLite>();
        }

        public SeasonSqLite(Shared.Remote.Season season, ShowSqLite show) : this()
        {
            Id = season.Id;
            Show = show;

            Update(season);
        }

        public void Update(Shared.Remote.Season season)
        {
            SeasonNumber = season.SeasonNumber;
            Blacklisted = season.Blacklisted;

            foreach (Episode episode in season.Episodes)
            {
                var bddEpisode = Episodes.SingleOrDefault(s => s.EpisodeNumber == episode.EpisodeNumber);
                if (bddEpisode == null)
                {
                    Episodes.Add(new EpisodeSqLite(episode, this));
                }
                else
                {
                    bddEpisode.Update(episode);
                }
            }

        }

        public Shared.Remote.Season Convert()
        {
            Shared.Remote.Season season = new Shared.Remote.Season(Id, Show.Convert())
            {
                SeasonNumber = SeasonNumber,
                Blacklisted = Blacklisted,
                Episodes = Episodes.Select(s => s.Convert()).ToList()
            };

            return season;
        }
    }

    public class EpisodeSqLite
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [ForeignKey("Season")]
        public virtual Guid SeasonID { get; set; }
        public virtual SeasonSqLite Season { get; set; }

        public virtual int EpisodeNumber { get; set; }

        [Required]
        public virtual EpisodeStatusSqLite Status { get; set; }

        public virtual string Name { get; set; }

        public virtual string PosterUrl { get; set; }

        public virtual string ProvidersData
        {
            get => JsonConvert.SerializeObject(Providers);
            set => Providers = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
        }

        /// <summary>
        /// Some cool description
        /// </summary>
        [NotMapped]
        public Dictionary<string, string> Providers { get; set; }


        public EpisodeSqLite()
        {

        }

        public EpisodeSqLite(Shared.Remote.Episode episode, SeasonSqLite season)
        {
            Id = episode.Id;
            Season = season;

            Update(episode);
        }

        public void Update(Shared.Remote.Episode episode)
        {
            EpisodeNumber = episode.EpisodeNumber;
            Providers = episode.Providers;
            //Status = episode.Status
            Name = episode.Name;

        }

        public Shared.Remote.Episode Convert()
        {
            Shared.Remote.Episode episode = new Shared.Remote.Episode(Id, Season.Convert())
            {
                EpisodeNumber = EpisodeNumber,
                Providers = Providers,
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
