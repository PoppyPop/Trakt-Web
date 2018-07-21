using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingApi _trackingApi;
        private readonly IImageApi _imageApi;
        private readonly IDatabase _database;

        public TrackingController(ITrackingApi trackingApi, IImageApi imageApi, IDatabase database)
        {
            _trackingApi = trackingApi;
            _database = database;
            _imageApi = imageApi;
        }

        // GET api/Tracking
        [HttpGet]
        public ActionResult<IEnumerable<Show>> Get()
        {
            return _database.GetMissingEpisode().Select(c => c.Convert()).ToList();
        }

        [HttpPost]
        public ActionResult<bool> Refresh()
        {
            var resutRefresh = _trackingApi.RefreshMissingEpisodes();
            if (resutRefresh)
                return _imageApi.RefreshImages();

            return false;
        }

        [HttpPost]
        [Route("ResetBlacklist")]
        public ActionResult<bool> ResetBlacklist()
        {
            return _database.ResetBlacklist();
        }

    }
}
