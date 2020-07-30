using HelpDesk.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Contracts
{
    public interface ITicketRepository : IRepositoryBase<TicketModel>
    {
        Task<IEnumerable<TicketModel>> GetAllTicket();
        Task<TicketModel> GetTicketById(Guid id);
        Task<IEnumerable<TicketModel>> GetTicketByCondition( Guid id );
        void CreateTicket(TicketModel ticket);
        void UpdateTicket(TicketModel ticket);
        void DeleteTicket(TicketModel ticket);
    }
}
