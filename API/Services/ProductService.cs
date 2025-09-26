using System.Net;
using API.Entities;
using API.Interfaces;
using API.Models.Base;
using API.Models.DTOs.Product;
using API.Models.Parameters.Product;
using AutoMapper;

namespace API.Services;

public class ProductService : BaseService<Product>, IProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repo, IMapper mapper) : base(repo)
    {
        _repo = repo;
        _mapper = mapper;
    }


    public async Task<ProductIdResponse> GetByIdAsync(ProductIdParameters parameters)
    {
        ProductIdResponse response = new();
        Product? product = await _repo.GetByIdAsync(parameters.Id, false);
        #region validation
        if (product == null)
            response.SetStatusCodeAndMessage(HttpStatusCode.NotFound,"Product is not found.");
        if (!response.IsSuccessAndValid())
        {
            return response;
        }
        #endregion
        response.Data = _mapper.Map<ProductDto>(product);
        return response;
    }

    public async Task<ProductCreateResponse> CreateAsync(ProductCreateParameters parameters)
    {
        return await this.UsingTransaction(async () =>
        {
            ProductCreateResponse response = new();

            #region validation
            if (parameters == null)
            {
                response.SetValidationMessage("Parameter Product is not found.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(parameters.Name))
                {
                    response.SetValidationMessage("Product Name cannot empty.");
                }
            }
            if (!response.IsSuccessAndValid())
            {
                return response;
            }
            #endregion

            Product product = _mapper.Map<Product>(parameters);
            await _repo.AddAsync(product);
            await _repo.SaveChangesAsync();

            response.Data = _mapper.Map<ProductDto>(product);
            return response;
        });
    }



    public async Task<ProductUpdateResponse> UpdateAsync(ProductUpdateParameters parameters)
    {
        ProductUpdateResponse response = new();
        Product? product = await _repo.GetByIdAsync(parameters.Id, true);

        #region Validation
        if (product == null)
        {
            response.SetStatusCodeAndMessage(404, "Product Not Found!");
            if (!response.IsSuccessAndValid())
            {
                return response;
            }
        }
        #endregion

        await _repo.SaveChangesAsync();
        return response;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id, false);
        if (product == null) throw new Exception("Product not found");

        _repo.Remove(product);
        await _repo.SaveChangesAsync();
    }

    public async Task<ProductListResponse> SearchAsync(ProductSearchParameters parameters)
    {
        ProductListResponse productListResponse = new ProductListResponse();

        #region validation
        if (parameters.SortBy == "string")
        {
            productListResponse.SetStatusCodeAndMessage(400, "validation hit!");
            if (!productListResponse.IsSuccessAndValid())
            {
                return productListResponse;
            }
        }
        #endregion


        BaseListResult<Product> listResult = await _repo.SearchAsync(parameters, false);
        productListResponse.Data = _mapper.Map<BaseListResult<ProductDto>>(listResult);
        return productListResponse;

    }

}
