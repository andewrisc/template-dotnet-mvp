using API.Entities;
using API.Interfaces;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.Product;
using API.Repository;
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

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _repo.GetReadonlyByConditionAsync(x => true, false);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id, false);
        return product == null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductResponse> CreateAsync(CreateProductDto dto)
    {
        return await this.UsingTransaction(async () =>
        {
            ProductResponse productResponse = new();
            Product product = _mapper.Map<Product>(dto);
            await _repo.AddAsync(product);
            await _repo.SaveChangesAsync();

            productResponse.Data = _mapper.Map<ProductDto>(product);
            return productResponse;
        });
    }

    public async Task UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _repo.GetByIdAsync(id, false);

        if (product == null) throw new Exception("Product not found");

        _mapper.Map(dto, product);
        _repo.Update(product);
        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id, false);
        if (product == null) throw new Exception("Product not found");

        _repo.Remove(product);
        await _repo.SaveChangesAsync();
    }
}
