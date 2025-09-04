using System;
using API.Models.Responses;

namespace API.Models.DTOs;

public class CreateProductDto : BaseParameters
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? Category { get; set; }
}
