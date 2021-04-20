using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int ID { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {
        }

        private Notification(Gig gig, NotificationType type)
        {
            if (gig == null)
                throw new ArgumentNullException();

            DateTime = DateTime.Now;
            Gig = gig;
            Type = type;
        }
        static public Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

        static public Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(newGig, NotificationType.GigUpdated);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;
            return notification;
        }

        static public Notification GigCancelled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCancelled);
        }
    }
}