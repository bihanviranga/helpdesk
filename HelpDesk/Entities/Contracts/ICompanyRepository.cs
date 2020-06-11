using System;
using System.Collections.Generic;
using HelpDesk.Entities.Models;

namespace HelpDesk.Entities.Contracts
{
    public interface ICompanyRepository : IRepositoryBase<CompanyModel>
    {
        IEnumerable<CompanyModel> GetAllCompanies();
        CompanyModel GetCompanyById(Guid id);
        void CreateCompany(CompanyModel company);
        void UpdateCompany(CompanyModel company);
        void DeleteCompany(CompanyModel company);
    }
}
