using Application.Common.Interfaces;

namespace Application.Customers.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery(int customerId) : IRequest<CustomerDto>;

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.customerId, cancellationToken);
            Guard.Against.Null(customer);

            return _mapper.Map<CustomerDto>(customer);
        }
    }
}