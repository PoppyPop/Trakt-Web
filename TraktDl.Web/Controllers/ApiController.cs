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

        // GET api/values
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

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
