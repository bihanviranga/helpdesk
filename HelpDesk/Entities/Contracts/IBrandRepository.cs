using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Entities.Models;

namespace HelpDesk.Entities.Contracts
{
    public interface IBrandRepository : IRepositoryBase<ProductdModel>
    {
        Task<IEnumerable<ProductdModel>> GetAllBrands();
        Task<ProductdModel> GetBrandById(string id);
        Task<IEnumerable<ProductdModel>> GetBrandsByCompanyId(string id);
        Task<IEnumerable<ProductdModel>> GetBrandsByCondition(string userType, string userCompanyId);
        void CreateBrand(ProductdModel brand);
        void UpdateBrand(ProductdModel brand);
        void DeleteBrand(ProductdModel brand);
    }
}
