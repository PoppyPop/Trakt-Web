using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;
using TraktDl.Web.Models;

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
        public ActionResult<IEnumerable<string>> Get()
        {
            var missings = _database.GetMissingEpisode();

            List<string> res = new List<string>();

            foreach (var missing in missings)
            {
                res.Add(missing.Season.Show.SerieName);
            }

            return res;
        }

        [HttpPost]
        public ActionResult<bool> Refresh()
        {
            return _trackingApi.RefreshMissingEpisodes();
        }

    }
}
