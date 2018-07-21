using System.Collections.Generic;
using TraktDl.Business.Database.SqLite;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Shared.Database
{
    public interface IDatabase
    {
        List<ApiKey> ApiKeys { get; }

        List<Show> Shows { get; }

        void AddApiKey(ApiKey apiKey);

        void AddOrUpdateShows(List<ShowSql> shows);

        List<Show> GetMissingEpisode();

        void ClearMissingEpisodes();

        bool ResetBlacklist();
    }
}
