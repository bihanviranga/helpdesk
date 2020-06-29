using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Entities.Repository
{
    public class TicketTimelineRepository : RepositoryBase<TicketTimelineModel>, ITicketTimelineRepository
    {
        public TicketTimelineRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }

        public void CreateTimelineEntry(TicketTimelineModel tktTimelineEntry)
        {
            Create(tktTimelineEntry);
        }

        public void DeleteTimelineEntry(TicketTimelineModel tktTimelineEntry)
        {
            Delete(tktTimelineEntry);
        }

        public async Task<IEnumerable<TicketTimelineModel>> GetEntriesByDate(DateTime txnDate)
        {
            return await FindByCondition(tmln => tmln.TxnDateTime.Equals(txnDate)).ToListAsync();
        }

        public async Task<IEnumerable<TicketTimelineModel>> GetEntriesByEvent(string ticketEvent)
        {
            return await FindByCondition(tmln => tmln.TktEvent.Equals(ticketEvent)).ToListAsync();
        }

        public async Task<IEnumerable<TicketTimelineModel>> GetEntriesByTicketId(Guid ticketId)
        {
            return await FindByCondition(tmln => tmln.TicketId.Equals(ticketId.ToString())).ToListAsync();
        }

        public async Task<IEnumerable<TicketTimelineModel>> GetEntriesByUsername(string username)
        {
            return await FindByCondition(tmln => tmln.TxnUserId.Equals(username)).ToListAsync();
        }

        public void UpdateTimelineEntry(TicketTimelineModel tktTimelineEntry)
        {
            Update(tktTimelineEntry);
        }
    }
}
