using System;
using System.Collections.Generic;

namespace HelpDesk.Entities.Models
{
    public partial class ConversationModel
    {
        public string CvId { get; set; }
        public string TicketId { get; set; }
        public string CvSender { get; set; }
        public string CvSenderType { get; set; }
        public DateTime CvSendDate { get; set; }
        public string CvContent { get; set; }
    }
}
