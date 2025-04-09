using Microsoft.AspNetCore.Mvc;
using MyWebApi2.Services;

namespace MyWebApi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("summaries")]
        public async Task<IActionResult> GetProductSummaries()
        {
            var summaries = await _productService.GetProductSummariesAsync();
            return Ok(summaries);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var paged = await _productService.GetPagedProductsAsync(page, pageSize);
            return Ok(paged);
        }
    }
}
