using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Musicly.Core.Models;
using Musicly.Core.ViewModel;
using Musicly.Persistence;
using Musicly.Core.Repositories;
using System.Security.Principal;

namespace Musicly.Controllers.APIs
{
    [System.Web.Http.Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ApplicationDbContext _db;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _db = new ApplicationDbContext();
        }
        
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttandees(id);

            if (gig == null)
                return NotFound();

            if (gig.IsCancel)
                return NotFound();

            if (gig.ArtistId != userId)
                return Unauthorized();

            gig.Cancel();

            _unitOfWork.SaveWork();
            return Ok();
        }

        [HttpPost]
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
