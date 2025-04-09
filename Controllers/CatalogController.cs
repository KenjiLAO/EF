using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/catalog")]
public class CatalogController : ControllerBase
{
    private readonly IProductCatalogService _productCatalogService;

    public CatalogController(IProductCatalogService productCatalogService)
    {
        _productCatalogService = productCatalogService;
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedProducts([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 20)
    {
        var result = await _productCatalogService.GetPagedProductsAsync(pageIndex, pageSize);
        return Ok(result);
    }
}
