using System;
using System.Collections.Generic;
using System.Text;

namespace TraktDl.Business.Shared.Database
{
    public enum EpisodeStatusSql
    {
        Unknown,
        Collected,
        Missing
    }

    public enum ProviderSql
    {
        Imdb,
        Tmdb
    }
}
