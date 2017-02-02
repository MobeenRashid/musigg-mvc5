using System.Collections.Generic;
using Musicly.Core.Models;

namespace Musicly.Core.Repositories
{
    public interface IGigsRepository
    {
        void AddGig(Gig gig);
        Gig GetGigOnId(int id);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttandees(int gigId);
        IEnumerable<Gig> GetUserGigs(string userId);
        IEnumerable<Gig> GetGigsOnSearchTerm(string searchTerm);

        IEnumerable<Gig> GetFutureGigs();
        Gig GetGigOnIdWithGenre(int id);
        Gig GetGigOnIdWithArtist(int id);
        Gig GetGigOnIdWithArtistAndGenre(int id);

    }
}