using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Musicly.Persistence;

namespace Musicly.Helpers
{
    public class UserHelper
    {
        private readonly ApplicationDbContext _db;

        public UserHelper()
        {
            _db = new ApplicationDbContext();
        }

        public string GetUserName()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
           var userName= _db.Users.Single(u => u.Id == userId);
            return userName.Name;
        }

        public int TotalUsers => _db.Users.Count();
    }
}