using System;
using Microsoft.AspNetCore.Mvc;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly IDatabase _database;
        private readonly ITrackingApi _trackingApi;

        public TrackingController(ITrackingApi trackingApi, IDatabase database)
        {
            _trackingApi = trackingApi;
            _database = database;
        }

        // GET: api/Tracking/isAuth
        [HttpGet("isAuth")]
        public bool Get()
        {
            try
            {
                _database.OpenTransaction();

                var result = _trackingApi.IsUsable(_database);

                _database.Commit();
                return result;
            }
            catch
            {
                _database.Rollback();
                throw;
            }
        }

        // GET: api/Tracking/token
        [HttpGet("token")]
        public DeviceToken GetToken()
        {
            try
            {
                _database.OpenTransaction();
                var result = _trackingApi.GetDeviceToken(_database).Result;

                _database.Commit();
                return result;

            }
            catch
            {
                _database.Rollback();
                throw;
            }
        }

        // POST: api/Tracking/token/{token}
        [HttpPost("token/{token}")]
        public bool Post(string token)
        {
            try
            {
                _database.OpenTransaction();
                var result = _trackingApi.CheckAuthent(_database, token).Result;

                _database.Commit();
                return result;

            }
            catch
            {
                _database.Rollback();
                throw;
            }
        }
    }
}
