using FluentNHibernate.Mapping;
using TraktDl.Business.Shared.Database;

namespace TraktDl.Business.Database.SqLite
{
    public class ApiKeyMap : ClassMap<ApiKeySql>
    {
        public ApiKeyMap()
        {
            Id(x => x.Id);
            Map(x => x.ApiData);
        }
    }
}
