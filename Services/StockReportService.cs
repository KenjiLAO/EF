using Microsoft.EntityFrameworkCore;

public class StockReportService
{
    private readonly ApplicationDbContext _context;

    public StockReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StockReportDto>> GetStockReportAsync()
    {
        var lowStockThreshold = 10;

        var stockData = await _context.Products
            .Where(p => p.Stock.Quantity <= lowStockThreshold)
            .Select(p => new StockReportDto
            {
                ProductId = p.ProductId,
                ProductName = p.Name,
                StockQuantity = p.Stock.Quantity,
                ReorderNeeded = p.Stock.Quantity <= lowStockThreshold
            })
            .ToListAsync();

        return stockData;
    }
}
