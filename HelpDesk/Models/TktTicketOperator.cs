using System;
using System.Collections.Generic;

namespace HelpDesk.Model
{
    public partial class TktTicketOperator
    {
        public string TktOperator { get; set; }
        public string TiketId { get; set; }
        public string SeqNo { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime AssignedBy { get; set; }
    }
}
