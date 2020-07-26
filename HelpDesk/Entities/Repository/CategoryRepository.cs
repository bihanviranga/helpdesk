using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Repository
{
    public class CategoryRepository : RepositoryBase<CategoryModel> , ICategoryRepository
    {
        public CategoryRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }
        public void CreateCategory(CategoryModel category)
        {
            Create(category);
        }

        public void DeleteCategory(CategoryModel category)
        {
            Delete(category);
        }
        public void UpdateCategory(CategoryModel category)
        {
            Update(category);
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            return await FindAll().OrderBy(c => c.CategoryId).ToListAsync();
        }

        public async Task<CategoryModel> GetCategoryById(String id)
        {
            return await FindByCondition(c => c.CategoryId.Equals(id.ToString())).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoriesByCompanyId(string id)
        {
            return await FindByCondition(c => c.CompanyId.Equals(id.ToString())).ToListAsync();
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoriesByCondition(string userType, string userCompanyId)
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
    }
}

