using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Musicly.Core.Models;
using Musicly.Core.Repositories;

namespace Musicly.Persistence.Repositories
{
    public class GigsRepository : IGigsRepository
    {
        private readonly ApplicationDbContext _db;

        public GigsRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        //add a new gig
        public void AddGig(Gig gig)
        {
            _db.Gigs.Add(gig);
        }

        //get gig with id 
        public Gig GetGigOnId(int id)
        {
            return _db.Gigs.Single(gg => gg.Id == id);
        }

        public Gig GetGigOnIdWithGenre(int id)
        {
            return _db.Gigs.Include(g => g.Genre).Single(gg => gg.Id == id);
        }
        public Gig GetGigOnIdWithArtist(int id)
        {
            return _db.Gigs.Include(g => g.Artist).Single(gg => gg.Id == id);
        }
        public Gig GetGigOnIdWithArtistAndGenre(int id)
        {
            return _db.Gigs.Include(g => g.Artist).Include(g=>g.Genre).Single(gg => gg.Id == id);
        }


        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _db.Attendances.Where(at => at.ArtistId == userId)
                .Select(at => at.Gig)
                .Include(art => art.Artist)
                .Include(gn => gn.Genre).ToList();
        }

        public Gig GetGigWithAttandees(int gigId)
        {
            return _db.Gigs.Include(g => g.Attendances.Select(a => a.Artist))
                       .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetUserGigs(string userId)
        {
            return _db.Gigs.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now)
                .Include(g => g.Genre).Include(g => g.Artist).ToList();
        }

        public IEnumerable<Gig> GetGigsOnSearchTerm(string searchTerm)
        {
            return _db.Gigs.Include(gig => gig.Artist).Include(gig => gig.Genre).Where(gig => gig.Artist.Name.Contains(searchTerm) || gig.Venue.Contains(searchTerm)
                                                    || gig.Genre.Name.Contains(searchTerm));
        }

        public IEnumerable<Gig> GetFutureGigs()
        {
            return _db.Gigs.Where(g => g.DateTime > DateTime.Now).Include(g => g.Genre).Include(g => g.Artist).ToList();
        }
    }
}