using System.Collections.Generic;
using Musicly.Core.Models;

namespace Musicly.Core.ViewModel
{
    public class UserProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public ICollection<Following> Followings { get; set; }
        public bool IsFollower { get; set; }
        public GigListViewModel GigListViewModel { get; set; }
    }
}