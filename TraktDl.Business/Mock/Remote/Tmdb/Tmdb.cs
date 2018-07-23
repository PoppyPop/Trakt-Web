using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
