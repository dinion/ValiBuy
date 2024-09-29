namespace Domain.Entities;

public class Customer
{
    public Customer()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Address = string.Empty;
        PostalCode = string.Empty;
    }

    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}