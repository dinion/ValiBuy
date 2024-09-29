namespace Domain.Entities;

public class Item
{
    public Item()
    {
        Order = new Order();
        Product = new Product();
    }

    public int ItemId { get; set; }
    public int Quantity { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public decimal TotalPrice => Quantity * Product.Price;
}