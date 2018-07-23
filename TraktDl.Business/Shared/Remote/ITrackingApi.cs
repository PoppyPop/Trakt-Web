using System.Collections.Generic;
using System.Threading.Tasks;

namespace TraktDl.Business.Shared.Remote
{
    public interface ITrackingApi
    {
        string GetMode { get; }

        /// <summary>
        /// Get the missing episodes
        /// </summary>
        /// <returns></returns>
        bool RefreshMissingEpisodes();

        bool IsUsable { get; }

        Task<DeviceToken> GetDeviceToken();

        Task<bool> CheckAuthent(string deviceToken);
    }
}
