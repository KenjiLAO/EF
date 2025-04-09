public interface IDashboardService
{
    Task<List<SalesDashboardDto>> GetSalesDashboardAsync();
}
