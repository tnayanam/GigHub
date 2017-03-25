using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        //is Nullable
        public DateTime? OrigninalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        
        [Required]
        public Gig Gig { get; set; }

    }
}