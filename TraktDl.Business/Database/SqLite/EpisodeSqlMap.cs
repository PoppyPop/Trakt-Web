using FluentNHibernate.Mapping;

namespace TraktDl.Business.Database.SqLite
{
    public class EpisodeSqlMap : ClassMap<EpisodeSql>
    {
        private const string UkName = "uk_episode";
        private const string IndexName = "idx_episode";

        public EpisodeSqlMap()
        {
            Id(x => x.Id)
                .GeneratedBy.GuidComb();
            References(x => x.Season)
                .UniqueKey(UkName)
                .Index(IndexName);
            Map(x => x.EpisodeNumber)
                .UniqueKey(UkName);
            Map(x => x.Status);
            Map(x => x.Name);
            Map(x => x.PosterUrl);
            Map(x => x.AirDate);
            Map(x => x.ProvidersData);


        }
    }
}
