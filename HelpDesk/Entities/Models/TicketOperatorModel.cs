using System;
using System.Collections.Generic;

namespace HelpDesk.Entities.Models
{
    public partial class TicketOperatorModel
    {
        public string TktOperator { get; set; }
        public string TicketId { get; set; }
        public int SeqNo { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime AssignedBy { get; set; }
    }
}
