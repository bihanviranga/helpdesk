﻿using System;
using System.Collections.Generic;

namespace HelpDesk.Entities.Models
{
    public partial class NotificationModel
    {
        public string NotifId { get; set; }
        public string TicketId { get; set; }
        public string NotifContent { get; set; }
        public string NotifUser { get; set; }
        public bool NotifRead { get; set; }
        public string NotifUrl { get; set; }
        public DateTime NotifDate { get; set; }

        public virtual UserModel NotifUserNavigation { get; set; }
        public virtual TicketModel Ticket { get; set; }
    }
}
