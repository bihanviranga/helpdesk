using System;
using System.Collections.Generic;

namespace HelpDesk.Model
{
    public partial class TktConversation
    {
        public string CvId { get; set; }
        public string TicketId { get; set; }
        public string CvSender { get; set; }
        public string CvSenderType { get; set; }
        public DateTime CvSendDate { get; set; }
    }
}
