using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Products.Queries.GetProductsWithPagination;

/// <summary>
/// Query to retrieve products with pagination.
/// </summary>
public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
{
    /// <summary>
    /// Gets or sets the page number for pagination. Default is 1.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets the size of each page for pagination. Default is 10.
    /// </summary>
    public int PageSize { get; set; } = 10;
}

/// <summary>
/// Handler for <see cref="GetProductsWithPaginationQuery"/> to process the retrieval of products with pagination.
/// </summary>
public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductsWithPaginationQueryHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work to manage data operations.</param>
    /// <param name="mapper">The mapper for converting entities to DTOs.</param>
    public GetProductsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the retrieval of products with pagination.
    /// </summary>
    /// <param name="request">The request containing pagination details.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing a paginated list of product DTOs.</returns>
    public async Task<PaginatedList<ProductDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var a = await _unitOfWork.Products.GetProductsWithPagination(request.PageNumber, request.PageSize);
        return _mapper.Map<PaginatedList<Product>, PaginatedList<ProductDto>>(await _unitOfWork.Products.GetProductsWithPagination(request.PageNumber, request.PageSize));
    }
}