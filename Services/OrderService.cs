using Microsoft.EntityFrameworkCore;
using MyWebApi2.Models;

public class OrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetOrdersWithDetailsAsync()
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                    .ThenInclude(p => p.Category)
            .ToListAsync();
    }
}
