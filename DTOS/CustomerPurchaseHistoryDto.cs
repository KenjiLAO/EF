public class CustomerPurchaseHistoryDto
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public decimal TotalSpent { get; set; }
    public List<OrderDto> PurchaseHistory { get; set; }
}

public class OrderDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
}
