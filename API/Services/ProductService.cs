using API.Entities;
using API.Interfaces;
using API.Models;
using API.Models.DTOs;
using AutoMapper;

namespace API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repo, IMapper mapper)
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
        var productList = await _repo.GetReadonlyByConditionAsync(x => x.Id == id, false);
        var product = productList.FirstOrDefault();
        return product == null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task CreateAsync(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        await _repo.AddAsync(product);
        await _repo.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UpdateProductDto dto)
    {
        var productList = await _repo.GetReadonlyByConditionAsync(x => x.Id == id, true);
        var product = productList.FirstOrDefault();
        if (product == null) throw new Exception("Product not found");

        _mapper.Map(dto, product);
        _repo.Update(product);
        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var productList = await _repo.GetReadonlyByConditionAsync(x => x.Id == id, true);
        var product = productList.FirstOrDefault();
        if (product == null) throw new Exception("Product not found");

        _repo.Remove(product);
        await _repo.SaveChangesAsync();
    }
}
