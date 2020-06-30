using System;

namespace HelpDesk.Entities.DataTransferObjects
{
    public partial class NotificationDto
    {
        public string NotifId { get; set; }
        public string TicketId { get; set; }
        public string NotifContent { get; set; }
        public string NotifUser { get; set; }
        public bool NotifRead { get; set; }
        public string NotifUrl { get; set; }
        public DateTime NotifDate { get; set; }
    }
}
