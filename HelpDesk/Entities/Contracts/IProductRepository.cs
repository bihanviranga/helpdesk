using HelpDesk.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Contracts
{
    public interface IProductRepository : IRepositoryBase<ProductModel>
    {
        Task<IEnumerable<ProductModel>> GetAllProducts();
        Task<ProductModel> GetProductById(String id);
        void CreateProduct(ProductModel product);
        void UpdateProduct(ProductModel product);
        void DeleteProduct(ProductModel product);

    }
}
