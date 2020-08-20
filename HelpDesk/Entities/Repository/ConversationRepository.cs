using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Repository
{
    public class ConversationRepository : RepositoryBase<ConversationModel>, IConversationRepository
    {
        public ConversationRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }
        public void CreateConversation(ConversationModel conversation)
        {
            conversation.CvId = Guid.NewGuid().ToString();
            Create(conversation);
        }

        public void DeleteConversation(ConversationModel conversation)
        {
            Delete(conversation);
        }

        public async Task<IEnumerable<ConversationModel>> GetConversationByTicketId(Guid id)
        {
            return await FindByCondition(c => c.TicketId.Equals(id.ToString())).OrderBy(t => t.CvSendDate).ToListAsync();
        }

        public void UpdateConversation(ConversationModel conversation)
        {
            Update(conversation);
        }
    }
}
