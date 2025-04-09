using Microsoft.EntityFrameworkCore;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CustomerPurchaseHistoryDto>> GetTopCustomersAsync(int top = 10)
    {
        var customers = await _context.Customers
            .Include(c => c.Orders)
                .ThenInclude(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Select(c => new CustomerPurchaseHistoryDto
            {
                CustomerId = c.CustomerId,
                Name = c.FullName,
                TotalSpent = c.Orders
                    .SelectMany(o => o.OrderItems)
                    .Sum(oi => oi.Quantity * oi.Price),
                PurchaseHistory = c.Orders
                    .Select(o => new OrderDto
                    {
                        OrderId = o.OrderId,
                        OrderDate = o.OrderDate,
                        TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.Price)
                    }).ToList()
            })
            .OrderByDescending(c => c.TotalSpent)
            .Take(top)
            .ToListAsync();

        return customers;
    }
}
