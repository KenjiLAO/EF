namespace MyWebApi2.DTOs
{
    public class OrderItemDto
    {
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal => UnitPrice * Quantity;
    }
}
