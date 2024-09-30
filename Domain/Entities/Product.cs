using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Products")]
public class Product : BaseAuditableEntity
{
    public Product()
    {
        Name = string.Empty;
    }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public List<Item> OrderItems { get; set; } = new List<Item>();
}