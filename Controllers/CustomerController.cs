using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("top-customers")]
    public async Task<IActionResult> GetTopCustomers()
    {
        var result = await _customerService.GetTopCustomersAsync();
        return Ok(result);
    }
}
