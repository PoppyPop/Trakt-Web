using System;
using System.Collections.Generic;

namespace TraktDl.Business.Database.SqLite
{
    public class SeasonSql
    {
        public virtual Guid Id { get; set; }

        public virtual ShowSql Show { get; set; }

        public virtual int SeasonNumber { get; set; }

        public virtual IList<EpisodeSql> Episodes { get; set; }

        public virtual bool Blacklisted { get; set; }


        public SeasonSql()
        {
            Episodes = new List<EpisodeSql>();
        }

        public SeasonSql(ShowSql show) : this()
        {
            Show = show;
            
        }
    }
}
