using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Entities.Repository
{
    public class BrandRepository : RepositoryBase<CompanyBrandModel>, IBrandRepository
    {
        public BrandRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext)
        {
        }

        public void CreateBrand(CompanyBrandModel brand)
        {
            Create(brand);
        }

        public void DeleteBrand(CompanyBrandModel brand)
        {
            Delete(brand);
        }

        public async Task<IEnumerable<CompanyBrandModel>> GetAllBrands()
        {
            return await FindAll().OrderBy(brd => brd.BrandName).ToListAsync();
        }

        public async Task<CompanyBrandModel> GetBrandById(string id)
        {
            return await FindByCondition(brd => brd.BrandId.Equals(id)).FirstOrDefaultAsync();
        }

        public void UpdateBrand(CompanyBrandModel brand)
        {
            Update(brand);
        }
    }
}