using API.Entities;

namespace API.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product?> GetByIdAsync(int productId, bool trackChanges);
}
