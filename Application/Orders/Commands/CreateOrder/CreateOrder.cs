using Application.Common.Interfaces;
using Application.Items.Queries.GetItems;
using Domain.Entities;

namespace Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<int>
{
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; private set; } = DateTime.Now;
    public List<ItemDto> Items { get; set; } = new List<ItemDto>();
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            CustomerId = command.CustomerId,
            OrderDate = command.OrderDate,
            TotalPrice = 0
        };

        foreach (var item in command.Items)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId, cancellationToken);
            Guard.Against.Null(product);

            var orderItem = new Item
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Order = order
            };

            order.OrderItems.Add(orderItem);
            order.TotalPrice += product.Price * item.Quantity;
        }

        await _unitOfWork.Orders.AddAsync(order);

        return order.Id;
    }
}