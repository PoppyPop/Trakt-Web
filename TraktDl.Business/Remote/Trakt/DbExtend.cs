using System;
using System.Collections.Generic;
using System.Text;
using TraktApiSharp.Objects.Get.Users;
using TraktDl.Business.Database.SqLite;
using TraktDl.Business.Shared.Database;

namespace TraktDl.Business.Remote.Trakt
{
    public static class DbExtend
    {
        public static void Update(this ShowSql showBdd, ITraktUserHiddenItem item)
        {
            showBdd.Blacklisted = true;

            var show = item.Show;

            showBdd.Providers[ProviderSql.Tmdb] = show.Ids.Tmdb.ToString();
            showBdd.Providers[ProviderSql.Imdb] = show.Ids.Imdb;
        }

        public static void Update(this ShowSql showBdd, TraktShow item)
        {
            showBdd.Name = item.SerieName;
            showBdd.Year = item.Year;

            showBdd.Providers[ProviderSql.Tmdb] = item.Tmdb.ToString();
            showBdd.Providers[ProviderSql.Imdb] = item.Imdb;
        }

        public static void Update(this SeasonSql seasonBdd, ITraktUserHiddenItem item)
        {
            seasonBdd.Blacklisted = true;
        }
    }
}
