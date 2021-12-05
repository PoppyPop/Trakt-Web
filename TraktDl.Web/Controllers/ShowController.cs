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
            try
            {
                _database.OpenTransaction();

                var result = _database.GetMissingEpisode().Select(c => c.Convert()).ToList();

                _database.Commit();
                return result;
            }
            catch
            {
                _database.Rollback();
                throw;
            }
        }

        // GET: api/Show/5
        [HttpGet("{id}", Name = "Get")]
        public Show Get(uint id)
        {
            try
            {
                _database.OpenTransaction();

                var showBdd = _database.GetShow(id)?.Convert();

                _database.Commit();

                return showBdd;
            }
            catch
            {
                _database.Rollback();
                throw;
            }
        }

        // POST: api/Show/5/Images
        [HttpPost("{id}/Images")]
        public Show Images(uint id)
        {
            try
            {
                _database.OpenTransaction();

                var result = _imageApi.RefreshImage(_database, id);
                //Show result = null;

                _database.Commit();

                return result;
            }
            catch
            {
                _database.Rollback();
                throw;
            }
        }

        // POST: api/Show
        [HttpPost]
        public ActionResult<bool> Refresh()
        {
            try
            {
                _database.OpenTransaction();

                var resutRefresh = _trackingApi.RefreshMissingEpisodes(_database);

                _database.Commit();

                return resutRefresh;
            }
            catch
            {
                _database.Rollback();
                throw;
            }
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
            try
            {
                _database.OpenTransaction();

                var result = _imageApi.RefreshImages(_database);

                _database.Commit();

                return result;
            }
            catch
            {
                _database.Rollback();
                throw;
            }
        }

        // POST: api/Show/ResetBlacklist
        [HttpPost]
        [Route("ResetBlacklist")]
        public ActionResult<bool> ResetBlacklist()
        {
            try
            {
                _database.OpenTransaction();

                var result = _database.ResetBlacklist();

                _database.Commit();

                return result;
            }
            catch
            {
                _database.Rollback();
                throw;
            }
        }

        // POST: api/Show/ResetImages
        [HttpPost]
        [Route("ResetImages")]
        public ActionResult<bool> ResetImages()
        {
            try
            {
                _database.OpenTransaction();

                var result = _database.ResetImages();

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
