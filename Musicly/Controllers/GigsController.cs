using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Musicly.Core.Models;
using Musicly.Core.ViewModel;
using Musicly.Persistence;
using Musicly.Persistence.Repositories;

namespace Musicly.Controllers
{
    public class GigsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public GigsController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        [Authorize]
        public ViewResult Mine()
        {
            var myUpcomingGigs = _unitOfWork.Gigs.GetUserGigs(User.Identity.GetUserId());

            return View(myUpcomingGigs);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = new ApplicationDbContext().Genres.ToList();
                return View("Edit", viewModel);
            }


            var gig = _unitOfWork.Gigs.GetGigWithAttandees(viewModel.Id);
            if (gig == null)
            {
                return HttpNotFound();
            }

            if (gig.ArtistId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre,viewModel.Description);

            _unitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Attending()
        {

            var userId = User.Identity.GetUserId();

            var gigViewModel = new GigListViewModel()
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                IsUserAuthenticated = User.Identity.IsAuthenticated,
                Attendances = _unitOfWork.Attendances.GetFutureAttendancesForUser(userId).ToLookup(at => at.GigId),
                Followings = _unitOfWork.Followings.GetFollowings(userId)
            };

            return View(gigViewModel);
        }



        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Genres = new ApplicationDbContext().Genres.ToList()
            };
            return View(viewModel);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var gigtoEdit = _unitOfWork.Gigs.GetGigOnId(id);

            if (gigtoEdit.ArtistId != userId)
            {
                return new HttpUnauthorizedResult();
            }

            var viewModel = new GigFormViewModel()
            {
                Id = id,
                Genres = new ApplicationDbContext().Genres.ToList(),
                Genre = gigtoEdit.GenreId,
                Date = gigtoEdit.DateTime.ToString("dd MMM yyyy"),
                Time = gigtoEdit.DateTime.ToString("HH:mm"),
                Venue = gigtoEdit.Venue,
                Description = gigtoEdit.Description
            };
            return View(viewModel);
        }



        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = new ApplicationDbContext().Genres.ToList();
                return View(viewModel);
            }

            var gig = new Gig
            {
                Venue = viewModel.Venue,
                GenreId = viewModel.Genre,
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                Description = viewModel.Description,
                DateAdded = DateTime.Now
            };

            _unitOfWork.Gigs.AddGig(gig);
            _unitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");
        }



        [HttpPost]
        public ActionResult Search(GigListViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new {searchTerm=viewModel.SearchTerm});
        }

        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGigOnIdWithArtistAndGenre(id);

            if (gig == null)
                return HttpNotFound();

            var gigDetialsView = new GigDetailsViewModel()
            {
                Gig = gig,
                ArtistName = _unitOfWork.Artists.GetArtistOnGig(gig.ArtistId).Name,
                IsFollower = _unitOfWork.Followings.IsFollower(User.Identity.GetUserId(), gig.ArtistId),
                IsGoing = _unitOfWork.Attendances.IsGoing(gig.Id, User.Identity.GetUserId()),
           
            };

            return View(gigDetialsView);
        }


    }
}