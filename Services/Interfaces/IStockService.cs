public interface IStockService
{
    Task<List<StockReportDto>> GetLowStockProductsAsync(int lowStockThreshold = 10);
}
