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
        Task<CategoryModel> GetCategoryById(String id , string companyId);
        Task<IEnumerable<CategoryModel>> GetCategoriesByCompanyId(String id);
        Task<IEnumerable<CategoryModel>> GetCategoriesByCondition(string userType, string userCompanyId);
        void CreateCategory(CategoryModel category);
        void DeleteCategory(CategoryModel category);
        void UpdateCategory(CategoryModel category);
    }
}
