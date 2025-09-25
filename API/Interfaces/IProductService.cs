using API.Models.DTOs.Product;
using API.Models.Parameters.Product;

namespace API.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<ProductResponse> CreateAsync(ProductCreateParameters dto);
    Task UpdateAsync(int id, ProductUpdateParameters dto);
    Task DeleteAsync(int id);
    Task<ProductListResponse> SearchAsync(ProductSearchParameters dto);
}
