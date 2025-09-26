using System;

namespace API.Models.Parameters.Product;

public class ProductCreateParameters
{
      public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public string? Category { get; set; }
}
