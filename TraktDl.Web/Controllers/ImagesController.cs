using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageApi _imageApi;

        public ImagesController(IImageApi imageApi)
        {
            _imageApi = imageApi;

        }

        // GET: api/Images/isAuth
        [HttpGet("isAuth")]
        public bool Get()
        {
            return _imageApi.IsUsable;
        }

        // GET: api/Images/token
        [HttpGet("token")]
        public DeviceToken GetToken()
        {
            return _imageApi.GetDeviceToken().Result;
        }

        // POST: api/Images/token/{token}
        [HttpPost("token/{token}")]
        public bool Post(string token)
        {
            return _imageApi.CheckAuthent(token).Result;
        }
    }
}
