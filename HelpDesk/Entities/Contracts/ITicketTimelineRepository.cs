using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Entities.Models;

namespace HelpDesk.Entities.Contracts
{
    public interface ITicketTimelineRepository : IRepositoryBase<TicketTimelineModel>
    {
        void CreateTimelineEntry(TicketTimelineModel tktTimelineEntry);
        void CreateTimelineEntry(string entryType, string ticketId, string userId);
        void UpdateTimelineEntry(TicketTimelineModel tktTimelineEntry);
        void DeleteTimelineEntry(TicketTimelineModel tktTimelineEntry);
        Task<IEnumerable<TicketTimelineModel>> GetEntriesByTicketId(Guid ticketId);
        Task<IEnumerable<TicketTimelineModel>> GetEntriesByEvent(string ticketEvent);
        Task<IEnumerable<TicketTimelineModel>> GetEntriesByUsername(string username);
        Task<IEnumerable<TicketTimelineModel>> GetEntriesByDate(DateTime txnDate);
    }
}
