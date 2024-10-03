using Application.Common.Interfaces;

namespace Application.Customers.Queries.GetCustomerById;

/// <summary>
/// Represents a query to get a customer by their ID.
/// </summary>
/// <param name="customerId">The ID of the customer to retrieve.</param>
public record GetCustomerByIdQuery(int customerId) : IRequest<CustomerDto>;

/// <summary>
/// Handler for the <see cref="GetCustomerByIdQuery"/>.
/// </summary>
public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCustomerByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work for data access.</param>
    /// <param name="mapper">The mapper for object mapping.</param>
    public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the retrieval of a customer by their ID.
    /// </summary>
    /// <param name="request">The request containing the customer ID.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="CustomerDto"/> representing the customer.</returns>
    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(request.customerId, cancellationToken);
        Guard.Against.Null(customer);

        return _mapper.Map<CustomerDto>(customer);
    }
}