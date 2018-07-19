using System.Collections.Generic;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Shared.Database
{
    public interface IDatabase
    {
        List<BlackListShow> BlackLists { get; }
        List<ApiKey> ApiKeys { get; }

        List<Show> Shows { get; }

        void AddApiKey(ApiKey apiKey);

        void AddOrUpdateShows(List<Show> shows);

        List<Episode> GetMissingEpisode();

        void ClearMissingEpisodes();

        void AddBlackList(BlackListShow blackListShow);
    }
}
