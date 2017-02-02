using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Musicly.Core.Models
{
    public class Attendance
    {
        [ForeignKey("GigId")]
        public Gig Gig { get; set; }

        [ForeignKey("ArtistId")]
        public ApplicationUser Artist { get; set; }

        [Key]
        [Column(Order = 1)]
        public int GigId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string ArtistId { get; set; }
    }
}