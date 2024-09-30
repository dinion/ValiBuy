using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Order")]
public class Order : BaseAuditableEntity
{
    public DateTime OrderDate { get; set; }

    public decimal TotalPrice { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public List<Item> OrderItems { get; set; } = new List<Item>();
}