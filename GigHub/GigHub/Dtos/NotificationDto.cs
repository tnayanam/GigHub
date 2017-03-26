using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;

namespace GigHub.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get;set; }
        //is Nullable
        public DateTime? OrigninalDateTime { get; set; }
        public string OriginalVenue { get;  set; }
        public GigDto Gig { get; set; }

    }
}