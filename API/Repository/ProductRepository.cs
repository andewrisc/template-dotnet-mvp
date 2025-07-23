using System;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
        
    }

    public async Task<Product?> GetByIdAsync(int productId, bool trackChanges)
    {
         return await this.FindByCondition(row => row.Id == productId, trackChanges).SingleOrDefaultAsync();
    }
}
