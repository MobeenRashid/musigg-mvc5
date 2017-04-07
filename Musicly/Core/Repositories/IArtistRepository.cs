using Musicly.Core.Models;
using System.Collections.Generic;

namespace Musicly.Core.Repositories
{
    public interface IArtistRepository
    {
       ApplicationUser GetArtistOnGig(string artistId);
       ICollection<Following> GetCurrentUserFollowers();
      
    } 
}