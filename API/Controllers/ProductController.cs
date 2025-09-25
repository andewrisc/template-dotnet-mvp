using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using API.Models.Parameters.Product;

namespace API.Controllers;

public class ProductController(IProductService service) : BaseController
{
    private readonly IProductService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] ProductCreateParameters parameters)
    {
        return ResponseToActionResult(await _service.CreateAsync(parameters));
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateParameters parameters)
    {
        try
        {
            await _service.UpdateAsync(id, parameters);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
