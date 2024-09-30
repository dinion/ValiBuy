using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Customer")]
public class Customer : BaseAuditableEntity
{

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public string PostalCode { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();
}