using Domain.Common;

namespace Domain.Entities;

public class Product : BaseAuditableEntity
{

    public string Name { get; set; }

    public decimal Price { get; set; }

    public List<Item> OrderItems { get; set; } = new List<Item>();
}