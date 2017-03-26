using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        protected Notification()
        {
                
        }

        private Notification(Gig gig, NotificationType type)
        {
            if(gig==null)
            {
                throw new ArgumentNullException("Gig");
            }
            Gig = gig;
            DateTime = DateTime.Now;
            Type = type;
        }
        public int Id { get; set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        //is Nullable
        public DateTime? OrigninalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }
        
        [Required]
        public Gig Gig { get; private set; }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string venue)
        {
            var notification=  new Notification(newGig, NotificationType.GigUpdated);
            notification.OriginalVenue = venue;
            notification.OrigninalDateTime = originalDateTime;

            return notification;

        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);
        }

    }
}