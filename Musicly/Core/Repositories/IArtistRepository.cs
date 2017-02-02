using Musicly.Core.Models;

namespace Musicly.Core.Repositories
{
    public interface IArtistRepository
    {
        ApplicationUser GetArtistOnGig(string artistId);
    }
}