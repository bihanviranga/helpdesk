using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Entities.Models;

namespace HelpDesk.Entities.Contracts
{
    public interface IBrandRepository : IRepositoryBase<CompanyBrandModel>
    {
        Task<IEnumerable<CompanyBrandModel>> GetAllBrands();
        Task<CompanyBrandModel> GetBrandById(string id);
        Task<IEnumerable<CompanyBrandModel>> GetBrandsByCompanyId(string id);
        Task<IEnumerable<CompanyBrandModel>> GetBrandsByCondition(string userType, string userCompanyId);
        void CreateBrand(CompanyBrandModel brand);
        void UpdateBrand(CompanyBrandModel brand);
        void DeleteBrand(CompanyBrandModel brand);
    }
}
