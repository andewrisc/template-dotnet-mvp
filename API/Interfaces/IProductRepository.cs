using API.Entities;
using API.Models.Base;
using API.Models.Parameters.Product;

namespace API.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product?> GetByIdAsync(int productId, bool trackChanges);
    Task<BaseListResult<Product>> SearchAsync(ProductSearchParameters parameters, bool trackChanges);
}
