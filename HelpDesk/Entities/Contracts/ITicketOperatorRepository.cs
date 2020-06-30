using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Entities.Models;

namespace HelpDesk.Entities.Contracts
{
    public interface ITicketOperatorRepository : IRepositoryBase<TicketOperatorModel>
    {
        void CreateTicketOperator(TicketOperatorModel ticketOperator);
        void UpdateTicketOperator(TicketOperatorModel ticketOperator);
        void DeleteTicketOperator(TicketOperatorModel ticketOperator);
        Task<IEnumerable<TicketOperatorModel>> GetByTicketId(Guid id);
        Task<IEnumerable<TicketOperatorModel>> GetByUsername(string username);
    }
}
