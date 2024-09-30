namespace Application.Customers.Queries.GetCustomerById
{
    public class CustomerDto
    {
        public CustomerDto()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = string.Empty;
            PostalCode = string.Empty;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
    }
}