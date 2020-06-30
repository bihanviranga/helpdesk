using System;
using System.Collections.Generic;

namespace HelpDesk.Entities.DataTransferObjects
{
    public partial class TicketOperatorDto
    {
        public string TktOperator { get; set; }
        public string TicketId { get; set; }
        public int SeqNo { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime AssignedBy { get; set; }
    }
}
