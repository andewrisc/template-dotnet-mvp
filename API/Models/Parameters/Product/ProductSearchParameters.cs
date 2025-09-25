using System;
using API.Models.Base;

namespace API.Models.Parameters.Product;

public class ProductSearchParameters : BaseParameters
{
    //add filter if needed
    public Filter Filter { get; set; }
    public ProductSearchParameters()
    {
        Filter = new Filter();
    }

}

public class Filter
{
    public string? Category { get; set; }
}
