using System;
using System.Linq;
using Musicly.Persistence;

namespace Musicly.Helpers
{
    public class MicHelper
    {
        private readonly ApplicationDbContext _db;

        public MicHelper()
        {
            _db = new ApplicationDbContext();
        }

        public int GetGigsCount()
        {
            return _db.Gigs.Count();
        }

        public int GetThisYearGigs()
        {
            var currentYear = DateTime.Today.Year;
            var count = _db.Gigs.Where(g => g.DateTime.Year == currentYear).ToList().Count;
            return count;
        }
    }
}