using FluentNHibernate.Mapping;

namespace TraktDl.Business.Database.SqLite
{
    public class ShowMap : ClassMap<ShowSql>
    {
        public ShowMap()
        {
            Id(x => x.Id);
            Map(x => x.Blacklisted);
            Map(x => x.Name);
            Map(x => x.Year);
            Map(x => x.PosterUrl);
            HasMany(x => x.Seasons)
                .Inverse()
                .Cascade.All();
            Map(x => x.ProvidersData);
        }
    }
}
