using Microsoft.AspNetCore.Mvc;
using MyWebApi2.Services.Interfaces;

[ApiController]
[Route("api/products")]
public class ProductSearchController : ControllerBase
{
    private readonly IProductSearchService _productSearchService;

    public ProductSearchController(IProductSearchService productSearchService)
    {
        _productSearchService = productSearchService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProducts([FromQuery] ProductSearchCriteria criteria)
    {
        var result = await _productSearchService.SearchProductsAsync(criteria);
        return Ok(result);
    }
}
