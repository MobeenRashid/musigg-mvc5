using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Musicly.Core.Models;
using Musicly.Core.Repositories;

namespace Musicly.Controllers
{
    [Authorize]
    public class ArtistsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArtistsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Artist
        public ActionResult Followings()
        {
            string userId = User.Identity.GetUserId();

            IEnumerable<ApplicationUser> followees = _unitOfWork.Followings.GetFollowings(userId)
                .Select(f => f.Followee);

            return View(followees);
        }


    }
}