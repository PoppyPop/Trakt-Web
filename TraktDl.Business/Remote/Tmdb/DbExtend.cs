using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;
using TraktApiSharp.Objects.Get.Users;
using TraktDl.Business.Database.SqLite;
using TraktDl.Business.Remote.Trakt;
using TraktDl.Business.Shared.Database;

namespace TraktDl.Business.Remote.Tmdb
{
    public static class DbExtend
    {
        public static void Update(this ShowSql showBdd, TMDbConfig config, TvShow item)
        {
            showBdd.PosterUrl = config.Images.BaseUrl + "w500" + item.PosterPath;

            if (item.ExternalIds != null)
                showBdd.Providers[ProviderSql.Imdb] = item.ExternalIds.ImdbId;
        }

        public static void Update(this EpisodeSql episodeBdd, TMDbConfig config, TvEpisode item)
        {
            episodeBdd.PosterUrl = config.Images.BaseUrl + "w300" + item.StillPath;
            episodeBdd.Name = item.Name;

            if (item.ExternalIds != null)
                episodeBdd.Providers[ProviderSql.Imdb] = item.ExternalIds.ImdbId;
        }
    }
}
