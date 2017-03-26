using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        protected Notification()
        {
                
        }

        public Notification(Gig gig, NotificationType type)
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
        public DateTime? OrigninalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        
        [Required]
        public Gig Gig { get; private set; }

    }
}