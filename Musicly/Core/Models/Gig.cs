using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Musicly.Core.Models
{
    public class Gig
    {
        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public int Id { get; set; }

        public bool IsCancel { get; private set; }

        public ApplicationUser Artist { get; set; }

        [ForeignKey("Artist")]
        public string ArtistId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public void Cancel()
        {
            IsCancel = true;

            var notification = Notification.GigCanceled(this);

            foreach (var attandee in Attendances.Select(a => a.Artist))
            {
                attandee.Notify(notification);
            }
        }


        public void Modify(DateTime date, string venue, int genre,string description)
        {
            var notification = Notification.GigUpdated(this, this.DateTime, this.Venue);

            Venue = venue;
            Description = description;
            DateTime = date;
            GenreId = genre;

            foreach (var attendee in Attendances.Select(u => u.Artist))
            {
                attendee.Notify(notification);
            }
        }
    }
}