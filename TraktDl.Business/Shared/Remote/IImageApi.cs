using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraktDl.Business.Shared.Database;

namespace TraktDl.Business.Shared.Remote
{
    public interface IImageApi
    {
        bool RefreshImages(IDatabase database);

        Show RefreshImage(IDatabase database,uint id);

        bool IsUsable(IDatabase database);

        Task<DeviceToken> GetDeviceToken(IDatabase database);

        Task<bool> CheckAuthent(IDatabase database, string deviceToken);
    }
}
