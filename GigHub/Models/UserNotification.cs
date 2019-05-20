using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order =1)]
        public string UserID { get; private set; }

        [Key]
        [Column(Order =2)]
        public int NotificationID { get; private set; }

        public bool IsRead { get; set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public UserNotification()
        {
        }

        public UserNotification(ApplicationUser user,Notification notification)
        {
            User = user ?? throw new ArgumentNullException("user");
            Notification = notification ?? throw new ArgumentNullException("notification");
        }
    }
}