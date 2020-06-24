using HelpDesk.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAllCategories();
        Task<CategoryModel> GetCategoryById(String id);
        void CreateCategory(CategoryModel category);
        void DeleteCategory(CategoryModel category);
    }
}
