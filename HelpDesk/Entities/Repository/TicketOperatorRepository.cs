using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Entities.Repository
{
    public class TicketOperatorRepository : RepositoryBase<TicketOperatorModel>, ITicketOperatorRepository
    {
        public TicketOperatorRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }

        public void CreateTicketOperator(TicketOperatorModel ticketOperator)
        {
            Create(ticketOperator);
        }

        public void DeleteTicketOperator(TicketOperatorModel ticketOperator)
        {
            Delete(ticketOperator);
        }

        public async Task<IEnumerable<TicketOperatorModel>> GetByTicketId(Guid id)
        {
            return await FindByCondition(tktOp => tktOp.TicketId.Equals(id.ToString())).ToListAsync();
        }

        public async Task<IEnumerable<TicketOperatorModel>> GetByUsername(string username)
        {
            return await FindByCondition(tktOp => tktOp.TktOperator.Equals(username)).ToListAsync();
        }

        public void UpdateTicketOperator(TicketOperatorModel ticketOperator)
        {
            Update(ticketOperator);
        }
    }
}