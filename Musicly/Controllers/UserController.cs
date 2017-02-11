using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Musicly.Core.Models;
using Musicly.Core.Repositories;
using Musicly.Core.ViewModel;
using Musicly.Infrastructure;

namespace Musicly.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        [HttpGet]
        public ActionResult Bio()
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.Users.Single(u => u.Id == userId);
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Bio(ApplicationUser user, HttpPostedFileBase avatarUpload)
        {
            if (!ModelState.IsValid)
                return View(user);

            var userInDb = await UserManager.FindByIdAsync(user.Id);
            userInDb.Email = user.Email;

            var emailResult = await UserManager.UserValidator.ValidateAsync(userInDb);

            if (emailResult.Succeeded)
            {
                userInDb.Name = user.Name;
                userInDb.Country = user.Country;
                userInDb.Address = user.Address;
                userInDb.About = user.About;
                userInDb.PhoneNumber = user.PhoneNumber;
                if (avatarUpload != null && avatarUpload.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(avatarUpload.InputStream))
                    {
                        userInDb.Avatar = binaryReader.ReadBytes(avatarUpload.ContentLength);
                    }
                }
                
                await UserManager.UpdateAsync(userInDb);
                return RedirectToAction("Index", "Home");
            }

            AddErrorsFromResult(emailResult.Errors);
            return View(user);
        }


        public async Task<ActionResult> UserProfile(string id)
        {
            
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
                return HttpNotFound();
            var gigListViewModel = new GigListViewModel
            {
                Attendances = _unitOfWork.Attendances.GetFutureAttendancesForUser(id).ToLookup(g => g.GigId),
                IsUserAuthenticated = User.Identity.IsAuthenticated,
                UpcomingGigs = _unitOfWork.Gigs.GetUserGigs(id)
            };
            var userViewModel = new UserProfileViewModel
            {
                User = user,
                Followings=_unitOfWork.Artists.GetCurrentUserFollowers(),
                IsFollower = _unitOfWork.Followings.IsFollower(User.Identity.GetUserId(), user.Id),
                GigListViewModel = gigListViewModel
            };

            return View(userViewModel);
        }

        private void AddErrorsFromResult(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [AllowAnonymous]
        public async Task<FileResult> Avatar(string id)
        {
            var user = await UserManager.Users.SingleAsync(u => u.Id == id);
            if (user?.Avatar != null)
                return File(user.Avatar, "image/png");

            var imagePath = Server.MapPath("~/Content/default_avatar_male.png");
            byte[] imgBytes;
            using (FileStream fileStream = System.IO.File.Open(imagePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    imgBytes = reader.ReadBytes((int)fileStream.Length);

                }
            }
            return File(imgBytes, "image/png");
        }
    }
}