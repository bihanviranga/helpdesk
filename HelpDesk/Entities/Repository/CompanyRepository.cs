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
            company.CompanyId = Guid.NewGuid().ToString();
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
            // Doesn't work in MSSQL without converting the Guid to a string.
            // Works that way in MySql though.
            // (?)
            // Probably because CompanyModel.CompanyId is defined as string, not Guid.
            // This happened because of the DB-First approach.
            return FindByCondition(cmp => cmp.CompanyId.Equals(id.ToString())).FirstOrDefault();
        }

        public void UpdateCompany(CompanyModel company)
        {
            Update(company);
        }
    }
}
