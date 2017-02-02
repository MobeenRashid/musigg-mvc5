﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Musicly.Core.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; private set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; private set; }

        [ForeignKey("NotificationId")]
        public Notification Notification { get; private set; }

        public bool IsRead { get; set; }

        protected UserNotification()
        {

        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));


            User = user;
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}