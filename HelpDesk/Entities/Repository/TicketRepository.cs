using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Repository
{
    public class TicketRepository : RepositoryBase<TicketModel>, ITicketRepository
    {
        public TicketRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }
        public void CreateTicket(TicketModel ticket)
        {
            ticket.TicketId = Guid.NewGuid().ToString();
            Create(ticket);
        }

        public void DeleteTicket(TicketModel ticket)
        {
            Delete(ticket);
        }

        public async Task<IEnumerable<TicketModel>> GetAllTicket()
        {
            return await FindAll().OrderBy(tkt => tkt.TktCreatedDate).ToListAsync();
        }

        public async Task<IEnumerable<TicketModel>> GetTicketByCondition(Guid id ,string userRole ,string userName)
        {
            if(userRole == "Manager")
            {
                return await FindByCondition(tkt => tkt.CompanyId.Equals(id.ToString())).ToListAsync();
            }else if (userRole == "Client")
            {
                return await FindByCondition(tkt => tkt.CompanyId == id.ToString() && tkt.TktCreatedBy == userName ).ToListAsync();
            }

            return null;
           
        }

        public async Task<TicketModel> GetTicketById(Guid id)
        {
            return await FindByCondition(tkt => tkt.TicketId.Equals(id.ToString()) ).FirstOrDefaultAsync();
        }

        public void UpdateTicket(TicketModel ticket)
        {
            Update(ticket);
        }
    }
}
