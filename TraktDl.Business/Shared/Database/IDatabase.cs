using System.Collections.Generic;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Shared.Database
{
    public interface IDatabase
    {
        List<ApiKey> ApiKeys { get; }

        List<Show> Shows { get; }

        void AddApiKey(ApiKey apiKey);

        void AddOrUpdateShows(List<Show> shows);

        List<Show> GetMissingEpisode();

        void ClearMissingEpisodes();
    }
}
