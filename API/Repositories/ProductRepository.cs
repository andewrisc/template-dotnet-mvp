using API.Data;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Models.Base;
using API.Models.Parameters.Product;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    public async Task<Product?> GetByIdAsync(int productId, bool trackChanges)
    {
        return await this.FindByCondition(row => row.Id == productId, trackChanges).SingleOrDefaultAsync();
    }

    public async Task<BaseListResult<Product>> SearchAsync(ProductSearchParameters parameters, bool trackChanges)
    {
        IQueryable<Product> query = this.FindAll(trackChanges);
        query = this.Filtering(query, parameters);
        return await query.ToListWithOrderByAndPagingAsync(parameters);
    }

    public IQueryable<Product> Filtering(IQueryable<Product> query, ProductSearchParameters parameters)
    {
        IQueryable<Product> queryResult = query;

        string? filterCategory = parameters?.Filter.Category?.ToLower();
        if (!string.IsNullOrEmpty(filterCategory))
        {
            queryResult = queryResult.Where(e => e.Category!.ToLower().Contains(filterCategory!));
        }

        return queryResult;
    }


}
