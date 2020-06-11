using System;
using System.Collections.Generic;
using System.Linq;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;

namespace HelpDesk.Entities.Repository
{
    public class CompanyRepository : RepositoryBase<CompanyModel>, ICompanyRepository
    {
        public CompanyRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }

        public void CreateCompany(CompanyModel company)
        {
            Create(company);
        }

        public void DeleteCompany(CompanyModel company)
        {
            Delete(company);
        }

        public IEnumerable<CompanyModel> GetAllCompanies()
        {
            return FindAll().OrderBy(cmp => cmp.CompanyName).ToList();
        }

        public CompanyModel GetCompanyById(Guid id)
        {
            var sid = id.ToString();
            var results = FindByCondition(cmp => cmp.CompanyId.Equals(sid)).FirstOrDefault();
            return results;
            // return FindByCondition(cmp => cmp.CompanyId.Equals(id)).FirstOrDefault();
        }

        public void UpdateCompany(CompanyModel company)
        {
            Update(company);
        }
    }
}