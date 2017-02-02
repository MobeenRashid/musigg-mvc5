using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Musicly.Core.Models;
using Musicly.Core.Repositories;

namespace Musicly.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _db;

        public FollowingRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public IEnumerable<Following> GetFollowings(string userId)
        {
            return _db.Followings.Where(f => f.FollowerId == userId).Include(f=>f.Followee).ToList();
        }

        public bool IsFollower(string userId,string artistId)
        {
            
            return _db.Followings.Any(f => f.FollowerId ==userId  && f.FolloweeId == artistId);
        }

    }
}