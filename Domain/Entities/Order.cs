using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Order : BaseAuditableEntity
{
    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    public decimal TotalPrice { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public List<Item> OrderItems { get; set; } = new List<Item>();
}