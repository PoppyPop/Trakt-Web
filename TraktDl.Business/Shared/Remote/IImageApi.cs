using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TraktDl.Business.Shared.Remote
{
    public interface IImageApi
    {
        bool RefreshImages();

        Show RefreshImage(uint id);

        bool IsUsable { get; }

        Task<DeviceToken> GetDeviceToken();

        Task<bool> CheckAuthent(string deviceToken);
    }
}
