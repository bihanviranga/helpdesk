using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.DataTransferObjects.Dashboard
{
    public class DashboardCompanyDetailsDto
    {
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
        public int TotalTickets { get; set; }
        public int TotalOpenTickets { get; set; }
        public int TotalClosedTickets { get; set; }
        public int TotalInprogressTickets { get; set; }
    }
}
