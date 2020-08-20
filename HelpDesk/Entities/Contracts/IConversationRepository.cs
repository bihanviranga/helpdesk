using HelpDesk.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Contracts
{
    public interface IConversationRepository : IRepositoryBase<ConversationModel>
    {
        Task<IEnumerable<ConversationModel>> GetConversationByTicketId(Guid id);
        void CreateConversation(ConversationModel conversation);
        void UpdateConversation(ConversationModel conversation);
        void DeleteConversation(ConversationModel conversation);
    }
}
