using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Musicly.Core.Models;
using Musicly.Core.Repositories;
using Musicly.Core.ViewModel;
using Musicly.Persistence;
using Musicly.Persistence.Repositories;

namespace Musicly.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly AttendanceRepository _attendanceRepository;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _attendanceRepository = new AttendanceRepository(new ApplicationDbContext());
        }

        public ActionResult Index(string searchTerm = null)
        {
            IEnumerable<Gig> allUpcomingGigs = !searchTerm.IsNullOrWhiteSpace() ? _unitOfWork.Gigs.GetGigsOnSearchTerm(searchTerm) : _unitOfWork.Gigs.GetFutureGigs();

            var userId = User.Identity.GetUserId();

            var attendances = _attendanceRepository.GetFutureAttendancesForUser(userId).ToLookup(at => at.GigId);

            var followings = _unitOfWork.Followings.GetFollowings(userId);



            GigListViewModel viewModel = new GigListViewModel()
            {
                UpcomingGigs = allUpcomingGigs,
                IsUserAuthenticated = User.Identity.IsAuthenticated,
                SearchTerm = searchTerm,
                Attendances = attendances,
                Followings = followings
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}