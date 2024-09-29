using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Item : BaseAuditableEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    [Required]
    public int Quantity { get; set; }
}