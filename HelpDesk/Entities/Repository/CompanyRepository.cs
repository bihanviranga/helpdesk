using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<CompanyModel>> GetAllCompanies()
        {
            return await FindAll().OrderBy(cmp => cmp.CompanyName).ToListAsync();
        }

        public async Task<IEnumerable<CompanyModel>> GetCompaniesByCondition(string userType, string userCompanyId)
        {
            if (userType == "Client")
            {
                return await FindByCondition(u => u.CompanyId.Equals(userCompanyId.ToString()))
                       .OrderBy(cmp => cmp.CompanyId).ToListAsync();
            }
            else    if (userType == "HelpDesk")
            {
                return await FindAll().OrderBy(cmp => cmp.CompanyId).ToListAsync();
            }

            return null;
        }

        public async Task<CompanyModel> GetCompanyById(Guid id)
        {
            // Doesn't work in MSSQL without converting the Guid to a string.
            // Works that way in MySql though.
            // (?)
            // Probably because CompanyModel.CompanyId is defined as string, not Guid.
            // This happened because of the DB-First approach.
            return await FindByCondition(cmp => cmp.CompanyId.Equals(id.ToString())).FirstOrDefaultAsync();
        }

        public async Task<List<CompanyDto>> GetCompanyDetails(IEnumerable<CompanyModel> companies)
        {
            var companiesDetails = new List<CompanyDto>();

            if(companies != null)
            {
                foreach (var company in companies)
                {
                    var companyDetails = new CompanyDto();
                    companyDetails.CompanyId = company.CompanyId;
                    companyDetails.CompanyName = company.CompanyName;

                    var numOfTickets = await HelpDeskContext.Set<TicketModel>()
                  .Where(t => t.CompanyId.Equals(company.CompanyId.ToString())).CountAsync();

                    var numOfProducts = await HelpDeskContext.Set<ProductModel>()
                  .Where(t => t.CompanyId.Equals(company.CompanyId.ToString())).CountAsync();

                    var numOfCategories = await HelpDeskContext.Set<CategoryModel>()
                  .Where(t => t.CompanyId.Equals(company.CompanyId.ToString())).CountAsync();

                    var numOfModules = await HelpDeskContext.Set<ModuleModel>()
                  .Where(t => t.CompanyId.Equals(company.CompanyId.ToString())).CountAsync();

                    var numOfBrands = await HelpDeskContext.Set<CompanyBrandModel>()
                  .Where(t => t.CompanyId.Equals(company.CompanyId.ToString())).CountAsync();

                    companyDetails.NumOfTickets = numOfTickets;
                    companyDetails.NumOfCategories = numOfCategories;
                    companyDetails.NumOfModules = numOfModules;
                    companyDetails.NumOfBrands = numOfBrands;
                    companyDetails.NumOfProducts = numOfProducts;

                    companiesDetails.Add(companyDetails);

                    
                }
                return companiesDetails;
            }
            return null;
        }

        public void UpdateCompany(CompanyModel company)
        {
            Update(company);
        }
    }
}
