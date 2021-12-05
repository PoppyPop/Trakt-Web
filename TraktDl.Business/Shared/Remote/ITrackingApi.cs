using System.Collections.Generic;
using System.Threading.Tasks;
using TraktDl.Business.Shared.Database;

namespace TraktDl.Business.Shared.Remote
{
    public interface ITrackingApi
    {
        string GetMode { get; }

        /// <summary>
        /// Get the missing episodes
        /// </summary>
        /// <returns></returns>
        bool RefreshMissingEpisodes(IDatabase database);

        bool IsUsable(IDatabase database);

        Task<DeviceToken> GetDeviceToken(IDatabase database);

        Task<bool> CheckAuthent(IDatabase database, string deviceToken);
    }
}
