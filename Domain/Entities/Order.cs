namespace Domain.Entities;

public class Order
{
    public Order()
    {
        Customer = new Customer();
    }

    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public ICollection<Item> Items { get; set; } = new List<Item>();
}