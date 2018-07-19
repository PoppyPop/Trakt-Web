using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TraktDl.Business.Shared.Remote;
using TraktDl.Web.Models;

namespace TraktDl.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingApi _trackingApi;

        public TrackingController(ITrackingApi trackingApi)
        {
            _trackingApi = trackingApi;
        }

        // GET api/Tracking
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var missing = _trackingApi.GetMissingEpisodes();

            List<string> res = new List<string>();

            foreach (Show show in missing)
            {
                res.Add(show.SerieName);
            }


            return res;
        }

    }
}
