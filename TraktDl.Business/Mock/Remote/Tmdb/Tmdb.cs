using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Mock.Remote.Tmdb
{
    public class Tmdb : IImageApi
    {
        public Tmdb()
        {
            
        }

        public bool RefreshImages(IDatabase database)
        {
            return true;
        }

        public Show RefreshImage(IDatabase database, uint id)
        {
            throw new NotImplementedException();
        }

        public bool IsUsable(IDatabase database) => true;

#pragma warning disable 1998
        public async Task<DeviceToken> GetDeviceToken(IDatabase database) => new DeviceToken();

        public async Task<bool> CheckAuthent(IDatabase database, string deviceToken) => true;

#pragma warning restore 1998
    }
}
