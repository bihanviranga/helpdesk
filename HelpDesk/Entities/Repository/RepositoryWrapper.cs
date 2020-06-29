using System.Threading.Tasks;
using HelpDesk.Entities.Contracts;

namespace HelpDesk.Entities.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly HelpDeskContext _helpDeskContext;
        private ICompanyRepository _company;
        private IUserRepository _user;
        private IArticleRepository _article;
        private IProductRepository _product;
        private IModuleRepository _module;
        private ICategoryRepository _category;
        private IResTemplateRepository _resTemplate;
        private INotificationRepository _notification;
        private ITicketOperatorRepository _ticketOperator;
        private ITicketTimelineRepository _ticketTimeline;

        public ICompanyRepository Company
        {
            get
            {
                if (_company == null)
                {
                    _company = new CompanyRepository(_helpDeskContext);
                }
                return _company;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_helpDeskContext);
                }
                return _user;
            }
        }

        public IArticleRepository Article
        {
            get
            {
                if (_article == null)
                {
                    _article = new ArticleRepository(_helpDeskContext);
                }
                return _article;
            }
        }

        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_helpDeskContext);
                }
                return _product;
            }
        }

        public IModuleRepository Module
        {
            get
            {
                if (_module == null)
                {
                    _module = new ModuleRepository(_helpDeskContext);
                }
                return _module;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_helpDeskContext);
                }
                return _category;
            }
        }

        public IResTemplateRepository ResTemplate
        {
            get
            {
                if (_resTemplate == null)
                {
                    _resTemplate = new ResTemplateRepository(_helpDeskContext);
                }
                return _resTemplate;
            }
        }

        public INotificationRepository Notification
        {
            get
            {
                if (_notification == null)
                {
                    _notification = new NotificationRepository(_helpDeskContext);
                }
                return _notification;
            }
        }

        public ITicketOperatorRepository TicketOperator
        {
            get
            {
                if (_ticketOperator == null)
                {
                    _ticketOperator = new TicketOperatorRepository(_helpDeskContext);
                }
                return _ticketOperator;
            }
        }

        public ITicketTimelineRepository TicketTimeline
        {
            get
            {
                if (_ticketTimeline == null)
                {
                    _ticketTimeline = new TicketTimelineRepository(_helpDeskContext);
                }
                return _ticketTimeline;
            }
        }

        public RepositoryWrapper(HelpDeskContext helpDeskContext)
        {
            _helpDeskContext = helpDeskContext;
        }

        public async Task Save()
        {
            await _helpDeskContext.SaveChangesAsync();
        }
    }
}
