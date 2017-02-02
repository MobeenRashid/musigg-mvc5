using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Musicly.Controllers;
using Musicly.Core.Models;

namespace Musicly.Core.ViewModel
{
    public class GigFormViewModel
    {
        [Required]
        public string Venue { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        public int Genre { get; set; }

        [StringLength(500)]
        public string Description { get; set; }


        public IEnumerable<Genre> Genres { get; set; }
        public string Action => (Id != 0) ? nameof(GigsController.Update) : nameof(GigsController.Create);

        public int Id { get; set; }

        public DateTime GetDateTime() => DateTime.Parse(String.Format($"{Date} {Time}"));
    }
}