using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.DataTransferObjects.Conversation
{
    public class ConversationDto
    {
        public string CvId { get; set; }
        public string TicketId { get; set; }
        public string CvSender { get; set; }
        public string CvSenderType { get; set; }
        public DateTime? CvSendDate { get; set; }
        public string CvContent { get; set; }
    }
}
