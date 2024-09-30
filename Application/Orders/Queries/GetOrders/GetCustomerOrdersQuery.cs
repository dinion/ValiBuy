using Application.Common.Interfaces;
using Application.Orders.Queries.GetOrdersWithPagination;
using Domain.Entities;

namespace Application.Orders.Queries.GetCustomerOrders;

public record GetCustomerOrdersQuery : IRequest<List<GetOrdersDto>>
{
    public int CustomerId { get; set; } = 0;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, List<GetOrdersDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerOrdersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetOrdersDto>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Order> orders = null;
        if (request.CustomerId > 0)
        {
            orders = await _unitOfWork.Orders
                .GetAllAsync(x => x.Where(o => o.CustomerId == request.CustomerId &&
                              (!request.StartDate.HasValue || o.OrderDate >= request.StartDate) &&
                              (!request.EndDate.HasValue || o.OrderDate <= request.EndDate))
                            .Include(x => x.OrderItems));

        }
        else
        {
            orders = await _unitOfWork.Orders
                .GetAllAsync(x => x.Where(o => (!request.StartDate.HasValue || o.OrderDate >= request.StartDate) && (!request.EndDate.HasValue || o.OrderDate <= request.EndDate)));
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