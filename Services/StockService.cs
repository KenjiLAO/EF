using Microsoft.EntityFrameworkCore;

public class StockService : IStockService
{
    private readonly ApplicationDbContext _context;

    public StockService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<StockReportDto>> GetLowStockProductsAsync(int lowStockThreshold = 10)
    {
        var stockData = await _context.Stocks
            .Include(s => s.Product)
            .Where(s => s.Quantity <= lowStockThreshold)
            .Select(s => new StockReportDto
            {
                ProductId = s.Product.ProductId,
                ProductName = s.Product.Name,
                StockQuantity = s.Quantity,
                ReorderNeeded = s.Quantity <= lowStockThreshold
            })
            .ToListAsync();

        return stockData;
    }
}
