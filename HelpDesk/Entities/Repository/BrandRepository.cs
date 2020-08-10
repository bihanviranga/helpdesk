using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Entities.Repository
{
    public class BrandRepository : RepositoryBase<ProductdModel>, IBrandRepository
    {
        public BrandRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext)
        {
        }

        public void CreateBrand(ProductdModel brand)
        {
            Create(brand);
        }

        public void DeleteBrand(ProductdModel brand)
        {
            Delete(brand);
        }

        public async Task<IEnumerable<ProductdModel>> GetAllBrands()
        {
            return await FindAll().OrderBy(brd => brd.BrandName).ToListAsync();
        }

        public async Task<ProductdModel> GetBrandById(string id)
        {
            return await FindByCondition(brd => brd.BrandId.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductdModel>> GetBrandsByCompanyId(string id)
        {
            return await FindByCondition(c => c.CompanyId.Equals(id.ToString())).ToListAsync();
        }

        public async Task<IEnumerable<ProductdModel>> GetBrandsByCondition(string userType, string userCompanyId)
        {
            if (userType == "Client")
            {
                return await FindByCondition(u => u.CompanyId.Equals(userCompanyId.ToString()))
                       .OrderBy(cmp => cmp.CompanyId).ToListAsync();
            }
            else if (userType == "HelpDesk")
            {
                return await FindAll().OrderBy(cmp => cmp.CompanyId).ToListAsync();
            }

            return null;
        }

        public void UpdateBrand(ProductdModel brand)
        {
            Update(brand);
        }
    }
}