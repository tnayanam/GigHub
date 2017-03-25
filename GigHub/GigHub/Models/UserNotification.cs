using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
        // we added this so that entity frameowkr can all the parameterized contructor. 
        //notice that below constructor is kept as protected because we do not want any EMPTY object
        // of usernotifcation to be created
        protected UserNotification()
        {

        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (notification == null)
                throw new ArgumentNullException("notification");

             User = user;
            Notification = notification;
        }
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }
        

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; set; }

        // I should not be able to change the value of "user" and "notification" 
        // once they are set int he constructor
        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool isRead { get; set; }
        
    }
}