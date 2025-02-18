using System.Threading.Tasks;

namespace HelpDesk.Entities.Contracts
{
    public interface IRepositoryWrapper
    {
        ICompanyRepository Company { get; }
        IUserRepository User { get; }
        IArticleRepository Article { get; }
        IProductRepository Product { get; }
        IModuleRepository Module { get; }
        ICategoryRepository Category { get; }
        IResTemplateRepository ResTemplate { get; }
        INotificationRepository Notification { get; }
        ITicketOperatorRepository TicketOperator { get; }
        ITicketTimelineRepository TicketTimeline { get; }
        ITicketRepository Ticket { get; }
        IBrandRepository Brand { get; }
        IConversationRepository Conversation { get; }
        IDashboardRepository Dashboard { get; }

        Task Save();
    }
}
