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

        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            return await FindAll().OrderBy(c => c.CategoryId).ToListAsync();
        }

        public async Task<CategoryModel> GetCategoryById(String id)
        {
            return await FindByCondition(c => c.CategoryId.Equals(id.ToString())).FirstOrDefaultAsync();
        }
    }
}

