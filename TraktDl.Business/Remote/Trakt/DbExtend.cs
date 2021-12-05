using System;
using System.Collections.Generic;
using System.Text;
using TraktDl.Business.Database.SqLite;
using TraktDl.Business.Shared.Database;
using TraktNet.Enums;
using TraktNet.Objects.Get.Users;

namespace TraktDl.Business.Remote.Trakt
{
    public static class DbExtend
    {
        public static void Update(this ShowSql showBdd, ITraktUserHiddenItem item)
        {
            showBdd.Blacklisted = true;

            if (item.Type == TraktHiddenItemType.Show)
            { 
            showBdd.Name = item.Show.Title;
            showBdd.Year = item.Show.Year;
            }
            else if (item.Type == TraktHiddenItemType.Movie)
            {
                showBdd.Name = item.Movie.Title;
                showBdd.Year = item.Movie.Year;
            }

            var show = item.Show;

            if (show.Ids.Tmdb.HasValue)
                showBdd.Providers[ProviderSql.Tmdb] = show.Ids.Tmdb.ToString();

            if (!string.IsNullOrEmpty(show.Ids.Imdb))
                showBdd.Providers[ProviderSql.Imdb] = show.Ids.Imdb;
        }

        public static void Update(this ShowSql showBdd, TraktShow item)
        {
            showBdd.Name = item.SerieName;
            showBdd.Year = item.Year;

            if (item.Tmdb.HasValue)
                showBdd.Providers[ProviderSql.Tmdb] = item.Tmdb.ToString();

            if (!string.IsNullOrEmpty(item.Imdb))
                showBdd.Providers[ProviderSql.Imdb] = item.Imdb;
        }

        public static void Update(this SeasonSql seasonBdd, ITraktUserHiddenItem item)
        {
            seasonBdd.Blacklisted = true;
        }
    }
}
