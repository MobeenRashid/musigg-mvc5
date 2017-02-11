using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Musicly.Core.Models;
using Musicly.Core.Repositories;

namespace Musicly.Persistence.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ApplicationDbContext _db;

        public ArtistRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ApplicationUser GetArtistOnGig(string artistId)
        {
           return _db.Users.SingleOrDefault(us => us.Id == artistId);
        }

        public ICollection<Following> GetCurrentUserFollowers()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
           return _db.Users.Where(ur => ur.Id == userId).Select(ur => ur.Followers).Single();
        }
    }
}