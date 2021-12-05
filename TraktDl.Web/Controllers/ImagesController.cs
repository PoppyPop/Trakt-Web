using Microsoft.AspNetCore.Mvc;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IDatabase _database;
        private readonly IImageApi _imageApi;

        public ImagesController(IImageApi imageApi, IDatabase database)
        {
            _imageApi = imageApi;
            _database = database;
        }

        // GET: api/Images/isAuth
        [HttpGet("isAuth")]
        public bool Get()
        {
            try
            {
                _database.OpenTransaction();

                var result = _imageApi.IsUsable(_database);

                _database.Commit();
                return result;
            }
            catch
            {
                _database.Rollback();
                throw;
            }
        }

        // GET: api/Images/token
        [HttpGet("token")]
        public DeviceToken GetToken()
        {
            try
            {
                _database.OpenTransaction();

                var result = _imageApi.GetDeviceToken(_database).Result;

                _database.Commit();
                return result;
            }
            catch
            {
                _database.Rollback();
                throw;
            }
        }

        // POST: api/Images/token/{token}
        [HttpPost("token/{token}")]
        public bool Post(string token)
        {
            try
            {
                _database.OpenTransaction();

                var result = _imageApi.CheckAuthent(_database, token).Result;

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
