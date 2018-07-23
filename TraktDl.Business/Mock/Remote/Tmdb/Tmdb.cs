using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Business.Mock.Remote.Tmdb
{
    public class Tmdb : IImageApi
    {
        public Tmdb()
        {
            
        }

        public bool RefreshImages()
        {
            return true;
        }

        public Show RefreshImage(uint id)
        {
            throw new NotImplementedException();
        }

        public bool IsUsable => true;

#pragma warning disable 1998
        public async Task<DeviceToken> GetDeviceToken() => new DeviceToken();

        public async Task<bool> CheckAuthent(string deviceToken) => true;

#pragma warning restore 1998
    }
}
