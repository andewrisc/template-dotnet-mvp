using API.Models.DTOs.Product;
using API.Models.Parameters.Product;

namespace API.Interfaces;

public interface IProductService
{
    Task<ProductIdResponse> GetByIdAsync(ProductIdParameters parameters);
    Task<ProductCreateResponse> CreateAsync(ProductCreateParameters parameters);
    Task<ProductUpdateResponse> UpdateAsync(ProductUpdateParameters parameters);
    Task<ProductListResponse> SearchAsync(ProductSearchParameters parameters);
}
