using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Customers.Commands.CreateCustomer;

/// <summary>
/// Represents a command to create a new customer.
/// </summary>
public class CreateCustomerCommand : IRequest<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCustomerCommand"/> class.
    /// </summary>
    public CreateCustomerCommand()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Address = string.Empty;
        PostalCode = string.Empty;
    }

    /// <summary>
    /// Gets or sets the first name of the customer.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the customer.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the address of the customer.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the postal code of the customer.
    /// </summary>
    public string PostalCode { get; set; }
}

/// <summary>
/// Handles the creation of a new customer.
/// </summary>
public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCustomerCommandHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work interface for database operations.</param>
    public CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the creation of a new customer.
    /// </summary>
    /// <param name="command">The command containing customer data.</param>
    /// <param name="cancellationToken">A cancellation token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with the customer's ID as the result.</returns>
    public async Task<int> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var entity = new Customer
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Address = command.Address,
            PostalCode = command.PostalCode,
        };

        await _unitOfWork.Customers.AddAsync(entity);

        return entity.Id;
    }
}
