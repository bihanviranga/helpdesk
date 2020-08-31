using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.DataTransferObjects.Ticket
{
    public class TicketAssignDto
    {
        public String UserName { get; set; }
        public String TicketId { get; set; }
    }
}
