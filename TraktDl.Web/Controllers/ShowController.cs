using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TraktDl.Business.Shared.Database;
using TraktDl.Business.Shared.Remote;

namespace TraktDl.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly IDatabase _database;
        private readonly ITrackingApi _trackingApi;
        private readonly IImageApi _imageApi;

        public ShowController(ITrackingApi trackingApi, IImageApi imageApi, IDatabase database)
        {
            _trackingApi = trackingApi;
            _database = database;
            _imageApi = imageApi;
        }

        // GET: api/Show
        [HttpGet]
        public IEnumerable<Show> Get()
        {
            return new List<Show>();
        }

        // GET: api/Show/Missings
        [HttpGet]
        [Route("Missings")]
        public IEnumerable<Show> Missings()
        {
            return _database.GetMissingEpisode().Select(c => c.Convert()).ToList();
        }

        // GET: api/Show/5
        [HttpGet("{id}", Name = "Get")]
        public Show Get(uint id)
        {
            var showBdd = _database.GetShow(id);

            return showBdd?.Convert();
        }

        // POST: api/Show/5/Images
        [HttpPost("{id}/Images")]
        public Show Images(uint id)
        {
            return _imageApi.RefreshImage(id);
        }

        // POST: api/Show
        [HttpPost]
        public ActionResult<bool> Refresh()
        {
            var resutRefresh = _trackingApi.RefreshMissingEpisodes();

            return resutRefresh;
        }


        // DELETE: api/Show/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // POST: api/Show/Images
        [HttpPost]
        [Route("Images")]
        public ActionResult<bool> Images()
        {
            return _imageApi.RefreshImages();
        }

        // POST: api/Show/ResetBlacklist
        [HttpPost]
        [Route("ResetBlacklist")]
        public ActionResult<bool> ResetBlacklist()
        {
            return _database.ResetBlacklist();
        }

        // POST: api/Show/ResetImages
        [HttpPost]
        [Route("ResetImages")]
        public ActionResult<bool> ResetImages()
        {
            return _database.ResetImages();
        }
    }
}
