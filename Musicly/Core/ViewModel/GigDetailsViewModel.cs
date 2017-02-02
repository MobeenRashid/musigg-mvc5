using Musicly.Core.Models;

namespace Musicly.Core.ViewModel
{
    public class GigDetailsViewModel
    {
        public Gig Gig { get; set; }
        public string ArtistName { get; set; }
        public bool IsFollower { get; set; }
        public bool IsGoing { get; set; }

    }
}