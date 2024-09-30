using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Customers.Queries.GetCustomerById;

namespace Application.Customers.Queries.GetCustomers
{
    public record GetCustomersQuery : IRequest<PaginatedList<CustomerDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PaginatedList<CustomerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _unitOfWork.Customers.GetAllAsync(x => x.Include(o => o.Orders.Where(x => x.CustomerId == x.Id)));

            //return _mapper.Map<PaginatedList<Customer>, PaginatedList<CustomerDto>>(customers);
            throw new NotImplementedException();
        }
    }
}