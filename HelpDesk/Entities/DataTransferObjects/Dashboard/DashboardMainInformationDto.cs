using HelpDesk.Entities.DataTransferObjects.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Models
{
    public class DashboardMainInformationDto
    {
        public DashboardMainInformationDto()
        {
            DashboardCompanyDeatails = new List<DashboardCompanyDetailsDto>();
        }
        public int TotalTickets { get; set; }
        public int OpenTickets { get; set; }
        public int ClosedTickets { get; set; }
        public int inprogress { get; set; }
        public virtual List<DashboardCompanyDetailsDto> DashboardCompanyDeatails { get; set; }

    }
}
