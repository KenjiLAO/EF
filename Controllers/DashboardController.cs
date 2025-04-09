using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("sales-by-category-month")]
    public async Task<IActionResult> GetSalesDashboard()
    {
        var result = await _dashboardService.GetSalesDashboardAsync();
        return Ok(result);
    }
}
