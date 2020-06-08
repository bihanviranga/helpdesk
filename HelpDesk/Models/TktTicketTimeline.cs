using System;
using System.Collections.Generic;

namespace HelpDesk.Model
{
    public partial class TktTicketTimeline
    {
        public string TicketId { get; set; }
        public DateTime TxnDateTime { get; set; }
        public string TktEvent { get; set; }
        public string TxnValues { get; set; }
        public string TxnUserId { get; set; }
        public string TxnUser { get; set; }
    }
}
