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
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingApi _trackingApi;

        public TrackingController(ITrackingApi trackingApi)
        {
            _trackingApi = trackingApi;

        }

        // GET: api/Tracking/isAuth
        [HttpGet("isAuth")]
        public bool Get()
        {
            return _trackingApi.IsUsable;
        }

        // GET: api/Tracking/token
        [HttpGet("token")]
        public DeviceToken GetToken()
        {
            return _trackingApi.GetDeviceToken().Result;
        }

        // POST: api/Tracking/token/{token}
        [HttpPost("token/{token}")]
        public bool Post(string token)
        {
            return _trackingApi.CheckAuthent(token).Result;
        }
    }
}
