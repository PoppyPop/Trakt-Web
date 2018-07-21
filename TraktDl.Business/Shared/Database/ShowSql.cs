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
                Providers = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(value))
                    Providers = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
            }
        }

        public Dictionary<string, string> Providers { get; set; }

        public ShowSql()
        {
            Providers = new Dictionary<string, string>();
        }

        public ShowSql(Shared.Remote.Show show) : this()
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
            PosterUrl = show.PosterUrl;

            foreach (Season showSeason in show.Seasons)
            {
                var bddSeason = Seasons.SingleOrDefault(s => s.SeasonNumber == showSeason.SeasonNumber);
                if (bddSeason == null)
                {
                    bddSeason = new SeasonSql(this);
                    Seasons.Add(bddSeason);
                }

                bddSeason.Update(showSeason);
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
                Providers = Providers,
                PosterUrl = PosterUrl,
            };

            foreach (var seasonSqLite in Seasons)
            {
                show.Seasons.Add(seasonSqLite.Convert());
            }


            return show;
        }
    }
}
