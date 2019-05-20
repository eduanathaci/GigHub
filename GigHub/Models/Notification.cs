using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Notification
    {
        public int ID { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public Gig Gig { get; set; }

        public Notification()
        {
        }

        public Notification(Gig gig,NotificationType Type)
        {
            Gig = gig ?? throw new ArgumentNullException("gig");
            DateTime = DateTime.Now;
            this.Type = Type;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig,NotificationType.GigCreated);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(newGig, NotificationType.GigUpdated);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }
    }
}