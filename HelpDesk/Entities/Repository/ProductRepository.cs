using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Repository
{
    public class ProductRepository : RepositoryBase<ProductModel>, IProductRepository
    {
        public ProductRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }
        public void CreateProduct(ProductModel product)
        {
            Create(product);
        }

        public void DeleteProduct(ProductModel product)
        {
            Delete(product);
        }

        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            return await FindAll().OrderBy(p => p.ProductId).ToListAsync();
        }

        public async Task<ProductModel> GetProductById(String id)
        {
            return await FindByCondition(p => p.ProductId.Equals(id.ToString())).FirstOrDefaultAsync();
        }

        public void UpdateProduct(ProductModel product)
        {
            Update(product);
        }
    }
}
