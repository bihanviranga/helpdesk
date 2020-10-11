using System;
using System.Collections.Generic;

namespace HelpDesk.Entities.Models
{
    public partial class TicketTimelineModel
    {
        public string TicketId { get; set; }
        public DateTime TxnDateTime { get; set; }
        public string TktEvent { get; set; }
        public string TxnValues { get; set; }
        public string TxnUserId { get; set; }

        public virtual UserModel TxnUser { get; set; }
    }
}
