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
        public string AssignedBy { get; set; }

        public virtual UserModel AssignedByNavigation { get; set; }
        public virtual TicketModel Ticket { get; set; }
        public virtual UserModel TktOperatorNavigation { get; set; }
    }
}
