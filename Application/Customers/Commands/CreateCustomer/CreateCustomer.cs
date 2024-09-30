using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<int>
{
    public CreateCustomerCommand()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Address = string.Empty;
        PostalCode = string.Empty;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

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