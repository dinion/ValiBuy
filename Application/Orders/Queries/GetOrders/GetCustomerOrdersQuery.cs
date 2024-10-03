using Application.Common.Interfaces;
using Application.Orders.Queries.GetOrdersWithPagination;
using Domain.Entities;

namespace Application.Orders.Queries.GetCustomerOrders;

/// <summary>
/// Represents a query to retrieve customer orders based on specified criteria.
/// </summary>
public record GetCustomerOrdersQuery : IRequest<List<GetOrdersDto>>
{
    /// <summary>
    /// Gets or sets the ID of the customer whose orders are to be retrieved.
    /// Default is 0.
    /// </summary>
    public int CustomerId { get; set; } = 0;

    /// <summary>
    /// Gets or sets the start date for filtering orders.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date for filtering orders.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets a specific date for filtering orders.
    /// </summary>
    public DateTime? SpecificDate { get; set; }
}

/// <summary>
/// Handles the <see cref="GetCustomerOrdersQuery"/> to retrieve orders for a specific customer.
/// </summary>
public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, List<GetOrdersDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCustomerOrdersQueryHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work to manage database operations.</param>
    public GetCustomerOrdersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the request to get customer orders based on the specified criteria.
    /// </summary>
    /// <param name="request">The request containing the filter criteria.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of <see cref="GetOrdersDto"/> representing the orders.</returns>
    public async Task<List<GetOrdersDto>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Order> orders = null;

        if (request.CustomerId > 0)
        {
            if (request.SpecificDate is not null && request.StartDate is null && request.EndDate is null)
            {
                orders = await _unitOfWork.Orders
                    .GetAllAsync(x => x.Where(o => o.CustomerId == request.CustomerId &&
                    o.OrderDate.Date == request.SpecificDate.Value.Date)
                    .Include(x => x.OrderItems));
            }
            else
            {
                orders = await _unitOfWork.Orders
                    .GetAllAsync(x => x.Where(o => o.CustomerId == request.CustomerId &&
                    (!request.StartDate.HasValue || o.OrderDate >= request.StartDate) &&
                    (!request.EndDate.HasValue || o.OrderDate <= request.EndDate))
                    .OrderBy(x => x.OrderDate)
                    .Include(x => x.OrderItems));
            }
        }
        else
        {
            orders = await _unitOfWork.Orders
                .GetAllAsync(x => x.Where(o => (!request.StartDate.HasValue || o.OrderDate >= request.StartDate) && (!request.EndDate.HasValue || o.OrderDate <= request.EndDate))
                .OrderBy(x => x.OrderDate)
                .Include(x => x.OrderItems));
        }

        return orders.Select(o => new GetOrdersDto
        {
            OrderId = o.Id,
            OrderDate = o.OrderDate,
            TotalPrice = o.TotalPrice,
            OrderItems = o.OrderItems.Select(x => new Items.Queries.GetItems.ItemDto
            {
                ItemId = x.Id,
                ProductId = x.ProductId,
                Quantity = x.Quantity
            })
        }).ToList();
    }
}