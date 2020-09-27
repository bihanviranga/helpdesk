using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects.Dashboard;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Repository
{
    public class DashboardRepository : RepositoryBase<DashboardModel>, IDashboardRepository
    {
        public DashboardRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }

        public async Task<DashboardModel> GetDashboardDetails(string userType)
        {
            var details = new DashboardModel();
            if (userType == "HelpDesk")
            {
                var totalTickets = await HelpDeskContext.Set<TicketModel>().CountAsync();

                var OpenTickets = await HelpDeskContext.Set<TicketModel>()
              .Where(t => t.TktStatus.Equals("Open".ToString())).CountAsync();

                var closedTickets = await HelpDeskContext.Set<TicketModel>()
              .Where(t => t.TktStatus.Equals("Closed".ToString())).CountAsync();

                var inprogressTickets = await HelpDeskContext.Set<TicketModel>()
              .Where(t => t.TktStatus.Equals("in-progress".ToString())).CountAsync();

                var companies = await HelpDeskContext.Set<CompanyModel>()
                    .ToListAsync();

                foreach(var company in companies)
                {
                    var t = new DashboardCompanyDetailsDto();

                    var totalTicketsInCompany = await HelpDeskContext.Set<TicketModel>()
                        .Where(t=>t.CompanyId.Equals(company.CompanyId)).CountAsync();

                    var OpenTicketsInCompany = await HelpDeskContext.Set<TicketModel>()
                  .Where(t => t.CompanyId.Equals(company.CompanyId) & t.TktStatus.Equals("Open".ToString())).CountAsync();

                    var closedTicketsInCompany = await HelpDeskContext.Set<TicketModel>()
                  .Where(t => t.CompanyId.Equals(company.CompanyId) & t.TktStatus.Equals("Closed".ToString())).CountAsync();

                    var inprogressTicketsInCompany = await HelpDeskContext.Set<TicketModel>()
                  .Where(t => t.CompanyId.Equals(company.CompanyId) & t.TktStatus.Equals("in-progress".ToString())).CountAsync();

                    t.CompanyId = company.CompanyId;
                    t.CompanyName = company.CompanyName;
                    t.TotalTickets = totalTicketsInCompany;
                    t.TotalOpenTickets = OpenTicketsInCompany;
                    t.TotalClosedTickets = closedTicketsInCompany;
                    t.TotalInprogressTickets = inprogressTicketsInCompany;

                    details.DashboardCompanyDeatails.Add(t);
                }

              

                details.ClosedTickets = closedTickets;
                details.OpenTickets = OpenTickets;
                details.TotalTickets = totalTickets;
                details.inprogress = inprogressTickets;

                return details;
               
            }

            return null;
        }
    }
}
