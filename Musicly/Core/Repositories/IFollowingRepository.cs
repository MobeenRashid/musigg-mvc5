using System.Collections.Generic;
using Musicly.Core.Models;

namespace Musicly.Core.Repositories
{
    public interface IFollowingRepository
    {
        IEnumerable<Following> GetFollowings(string userId);
        bool IsFollower(string userId,string artistId);
    }
}