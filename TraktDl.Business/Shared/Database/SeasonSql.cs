using System;
using System.Collections.Generic;
using System.Linq;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Database.SqLite
{
    public class SeasonSql
    {
        public virtual Guid Id { get; set; }

        public virtual uint ShowID { get; set; }
        public virtual ShowSql Show { get; set; }

        public virtual int SeasonNumber { get; set; }

        public virtual List<EpisodeSql> Episodes { get; set; }

        public virtual bool Blacklisted { get; set; }


        public SeasonSql()
        {

        }

        public SeasonSql(ShowSql show) : this()
        {
            Show = show;
            Episodes = new List<EpisodeSql>();
        }

        public Shared.Remote.Season Convert()
        {
            Shared.Remote.Season season = new Shared.Remote.Season(Id)
            {
                SeasonNumber = SeasonNumber,
                Blacklisted = Blacklisted,
                Episodes = Episodes.Select(s => s.Convert()).ToList(),
            };


            return season;
        }
    }
}
