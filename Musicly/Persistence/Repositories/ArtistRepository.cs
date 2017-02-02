using System.Linq;
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
    }
}