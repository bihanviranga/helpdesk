using HelpDesk.Entities.Contracts;

namespace HelpDesk.Entities.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private HelpDeskContext _helpDeskContext;
        private ICompanyRepository _company;

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

        public RepositoryWrapper(HelpDeskContext helpDeskContext)
        {
            _helpDeskContext = helpDeskContext;
        }

        public void Save()
        {
            _helpDeskContext.SaveChanges();
        }
    }
}
