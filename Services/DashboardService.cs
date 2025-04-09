using Microsoft.EntityFrameworkCore;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _context;

    public DashboardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SalesDashboardDto>> GetSalesDashboardAsync()
    {
        var salesData = await _context.OrderItems
            .Include(oi => oi.Product)
            .ThenInclude(p => p.Category)
            .Where(oi => oi.Order.OrderDate >= DateTime.Now.AddMonths(-12))
            .GroupBy(oi => new { oi.Product.Category.Name, Month = oi.Order.OrderDate.Month })
            .Select(g => new SalesDashboardDto
            {
                Category = g.Key.Name,
                Month = g.Key.Month,
                TotalSales = g.Sum(oi => oi.Quantity * oi.Price)
            })
            .ToListAsync();

        return salesData;
    }
}
