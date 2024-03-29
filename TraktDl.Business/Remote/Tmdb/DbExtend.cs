﻿using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;
using TraktDl.Business.Database.SqLite;
using TraktDl.Business.Remote.Trakt;
using TraktDl.Business.Shared.Database;

namespace TraktDl.Business.Remote.Tmdb
{
    public static class DbExtend
    {
        public static void Update(this ShowSql showBdd, TMDbConfig config, TvShow item)
        {
            showBdd.PosterUrl = config.Images.SecureBaseUrl + "w500" + item.PosterPath;

            if (item.ExternalIds != null)
                showBdd.Providers[ProviderSql.Imdb] = item.ExternalIds.ImdbId;
        }

        public static void Update(this EpisodeSql episodeBdd, TMDbConfig config, TvEpisode item)
        {
            episodeBdd.PosterUrl = config.Images.SecureBaseUrl + "w300" + item.StillPath;
            episodeBdd.Name = item.Name;
            episodeBdd.AirDate = item.AirDate;

            if (item.ExternalIds != null)
                episodeBdd.Providers[ProviderSql.Imdb] = item.ExternalIds.ImdbId;
        }
    }
}
