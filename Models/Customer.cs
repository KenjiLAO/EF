namespace MyWebApi2.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public ICollection<Order> Orders { get; set; }
}