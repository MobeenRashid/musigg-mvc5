using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Musicly.Core.Models;
using Musicly.Core.ViewModel;
using Musicly.Persistence;

namespace Musicly.Controllers.APIs
{
    [System.Web.Http.Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _db;

        public GigsController()
        {
            _db = new ApplicationDbContext();
        }
        
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _db.Gigs.Include(g => g.Attendances.Select(a => a.Artist)).Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCancel)
                return NotFound();

            gig.Cancel();

            _db.SaveChanges();
            return Ok();
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {

                var errorsList = ModelState.ToDictionary(kyp => kyp.Key,
                    kyp => kyp.Value.Errors.Select(er => er.ErrorMessage));

                var httpResponce = Request.CreateResponse(HttpStatusCode.BadRequest, errorsList);

                return httpResponce;
            }
            var gig = new Gig()
            {
                Venue = viewModel.Venue,
                GenreId = viewModel.Genre,
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),

            };

            _db.Gigs.Add(gig);
            _db.SaveChanges();
            var httpResponceOk = Request.CreateResponse(HttpStatusCode.OK);

            return httpResponceOk;
        }
    }
}
