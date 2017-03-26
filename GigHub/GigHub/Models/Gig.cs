using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GigHub.ViewModels;

namespace GigHub.Models
{
    public class Gig
    {
        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public int Id { get; set; }

        public ApplicationUser Artist { get; set; }

        public DateTime DateTime { get; set; }

        public bool IsCanceled { get; private set; }
        //public bool IsUpdated { get; private set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public string ArtistId { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public void Cancel()
        {
            IsCanceled = true;
            var notification = Notification.GigCanceled(this);
            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }

        public void Modify(DateTime dateTime, byte genre, string venue)
        {
            // setting old values
            var notification = Notification.GigUpdated(this, dateTime, venue);
            // updating to new values
            Venue = venue;
            DateTime = dateTime;
            GenreId = genre;
            foreach (var attendee in Attendances.Select(a=>a.Attendee))
            {
                attendee.Notify(notification);
            }

        }
    }
}