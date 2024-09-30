using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Products.Queries.GetProductsWithPagination;

public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<PaginatedList<Product>, PaginatedList<ProductDto>>(await _unitOfWork.Products.GetProductsWithPagination(request.PageNumber, request.PageSize));
    }
}