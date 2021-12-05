using FluentNHibernate.Mapping;

namespace TraktDl.Business.Database.SqLite
{
    public class SeasonMap : ClassMap<SeasonSql>
    {
        private const string UkName = "uk_season";
        private const string IndexName = "idx_season";

        public SeasonMap()
        {
            Id(x => x.Id)
                .GeneratedBy.GuidComb();
            References(x => x.Show)
                .UniqueKey(UkName)
                .Index(IndexName);
            Map(x => x.SeasonNumber)
                .UniqueKey(UkName);

            HasMany(x => x.Episodes)
                .Inverse()
                .Cascade.All();

            Map(x => x.Blacklisted);
        }
    }
}
