using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {
        public int ID { get; set; }

        public bool IsCanceled { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistID { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public DateTime DateTime { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreID { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

       

        internal void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.GigCanceled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
        

        internal void Modify(string venue, byte genreID, DateTime dateTime)
        {
            var notification =Notification.GigUpdated(this,dateTime,venue);

            Venue = venue;
            GenreID = genreID;
            DateTime = dateTime;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }

        }
    }


}