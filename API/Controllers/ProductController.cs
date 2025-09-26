using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using API.Models.Parameters.Product;

namespace API.Controllers;

public class ProductController(IProductService service) : BaseController
{
    private readonly IProductService _service = service;


    // [HttpGet("{id}")]
    [HttpPost("get-by-id")]
    public async Task<IActionResult> GetById(ProductIdParameters parameters)
    {
        ProductIdResponse response = await DoServiceAny(parameters, async (svcParameters) =>
        {
            return await _service.GetByIdAsync(parameters);
        });
        return ResponseToActionResult(response);

    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] ProductCreateParameters parameters)
    {
        ProductCreateResponse response = await DoServiceAny(parameters, async (svcParameters) =>
        {
            return await _service.CreateAsync(parameters);
        });
        return ResponseToActionResult(response);
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] ProductSearchParameters parameters)
    {
        ProductListResponse response = await DoService(parameters, async (svcParameters) =>
        {
            return await _service.SearchAsync(parameters);
        });
        return ResponseToActionResult(response);

    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] ProductUpdateParameters parameters)
    {
        ProductUpdateResponse response = await DoServiceAny(parameters, async (svcParameters) =>
        {
            return await _service.UpdateAsync(parameters);;
        });
        return ResponseToActionResult(response);
       
    }

   
}
