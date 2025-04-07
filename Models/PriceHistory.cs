namespace MyWebApi2.Models;

public class PriceHistory
{
    public int PriceHistoryId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public decimal Price { get; set; }
    public DateTime DateChanged { get; set; }
}