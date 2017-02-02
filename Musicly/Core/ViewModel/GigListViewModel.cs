using System.Collections.Generic;
using System.Linq;
using Musicly.Core.Models;

namespace Musicly.Core.ViewModel
{
    public class GigListViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool IsUserAuthenticated { get; set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendance> Attendances { get; set; }
        public IEnumerable<Following> Followings { get; set; }
    }
}