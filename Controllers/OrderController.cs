using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetOrderDetails()
    {
        var orders = await _orderService.GetOrdersWithDetailsAsync();
        return Ok(orders);
    }
}
