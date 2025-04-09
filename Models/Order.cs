namespace MyWebApi2.Models;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }

    public decimal TotalAmount
    {
        get
        {
            return OrderItems.Sum(oi => oi.Quantity * oi.Price);
        }
    }
    public Customer Customer { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}