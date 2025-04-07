namespace MyWebApi2.Models;

public class Stock
{
    public int StockId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public DateTime LastUpdated { get; set; }
}