using System.Threading.Tasks;
using HelpDesk.Entities.Contracts;

namespace HelpDesk.Entities.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private HelpDeskContext _helpDeskContext;
        private ICompanyRepository _company;
        private IUserRepository _user;

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
