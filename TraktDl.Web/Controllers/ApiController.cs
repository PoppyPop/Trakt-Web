using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly IDatabase _database;

        public TrackingController(ITrackingApi trackingApi, IDatabase database)
        {
            _trackingApi = trackingApi;
            _database = database;
        }

        // GET api/Tracking
        [HttpGet]
        public ActionResult<IEnumerable<Show>> Get()
        {
            return _database.GetMissingEpisode();
        }

        [HttpPost]
        public ActionResult<bool> Refresh()
        {
            return _trackingApi.RefreshMissingEpisodes();
        }

    }
}
