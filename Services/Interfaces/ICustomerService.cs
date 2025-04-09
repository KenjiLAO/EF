public interface ICustomerService
{
    Task<List<CustomerPurchaseHistoryDto>> GetTopCustomersAsync(int top = 10);
}
