using Application.Common.Interfaces;
using Application.Items.Queries.GetItems;
using Domain.Entities;

namespace Application.Orders.Commands.CreateOrder;

/// <summary>
/// Represents a command to create a new order.
/// </summary>
public class CreateOrderCommand : IRequest<int>
{
    /// <summary>
    /// Gets or sets the ID of the customer placing the order.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets the date the order was created.
    /// </summary>
    public DateTime OrderDate { get; private set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the list of items included in the order.
    /// </summary>
    public List<ItemDto> Items { get; set; } = new List<ItemDto>();
}

/// <summary>
/// Handler for the <see cref="CreateOrderCommand"/>.
/// </summary>
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateOrderCommandHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work for data access.</param>
    public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the creation of a new order.
    /// </summary>
    /// <param name="command">The command containing the details of the order to create.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with the ID of the created order as the result.</returns>
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