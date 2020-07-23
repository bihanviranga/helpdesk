using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Entities.Models;

namespace HelpDesk.Entities.Contracts
{
    public interface ICompanyRepository : IRepositoryBase<CompanyModel>
    {
        Task<IEnumerable<CompanyModel>> GetAllCompanies();
        Task<CompanyModel> GetCompanyById(Guid id);
        Task<IEnumerable<CompanyModel>> GetCompaniesByCondition(string userType, string userCompanyId);
        void CreateCompany(CompanyModel company);
        void UpdateCompany(CompanyModel company);
        void DeleteCompany(CompanyModel company);
    }
}
