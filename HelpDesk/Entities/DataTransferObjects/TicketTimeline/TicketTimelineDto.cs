using System;

namespace HelpDesk.Entities.DataTransferObjects
{
    public partial class TicketTimelineDto
    {
        public string TicketId { get; set; }
        public DateTime TxnDateTime { get; set; }
        public string TktEvent { get; set; }
        public string TxnValues { get; set; }
        public string TxnUserId { get; set; }
    }
}
