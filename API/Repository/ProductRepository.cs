using System;
using API.Data;
using API.Entities;
using API.Interfaces;

namespace API.Repository;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
        //add if any custom 
    }
}
