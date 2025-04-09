public class StockReportDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int StockQuantity { get; set; }
    public bool ReorderNeeded { get; set; }
}
