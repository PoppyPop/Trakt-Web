using System.Collections.Generic;
using TraktDl.Business.Database.SqLite;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Shared.Database
{
    public interface IDatabase
    {
        void AddApiKey(ApiKeySql apiKey);

        ApiKeySql GetApiKey(string name);

        void AddOrUpdateShows(List<ShowSql> shows);

        List<ShowSql> GetShows();

        ShowSql GetShow(uint id);

        List<ShowSql> GetMissingEpisode();

        List<ShowSql> GetMissingImages();

        void ClearMissingEpisodes();

        bool ResetBlacklist();

        bool ResetImages();
    }
}
