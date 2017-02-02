using System.ComponentModel.DataAnnotations;

namespace Musicly.Core.ViewModel
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}