namespace Domain.Entities;

public class Product
{
    public Product()
    {
        Name = string.Empty;
    }

    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public ICollection<Item> Items { get; set; } = new List<Item>();
}