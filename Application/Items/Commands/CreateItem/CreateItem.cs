using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Items.Commands.CreateItem;

/// <summary>
/// Represents a command to create a new item associated with an order.
/// </summary>
public record CreateItemCommand : IRequest<int>
{
    /// <summary>
    /// Gets or sets the ID of the associated order.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the product to be added to the order.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product to be added to the order.
    /// </summary>
    public int Quantity { get; set; }
}

/// <summary>
/// Handler for the <see cref="CreateItemCommand"/>.
/// </summary>
public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateItemCommandHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work for data access.</param>
    public CreateItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the creation of a new item associated with an order.
    /// </summary>
    /// <param name="request">The request containing the item details.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The ID of the created item or -1 if the order or product is not found.</returns>
    public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
            return -1;

        var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            return -1;

        var entity = new Item
        {
            OrderId = request.OrderId,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };

        await _unitOfWork.Items.AddAsync(entity);

        return entity.OrderId; // Consider returning entity.Id for the created item instead of OrderId.
    }
}
