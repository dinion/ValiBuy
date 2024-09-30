using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Items.Commands.CreateItem;

public record CreateItemCommand : IRequest<int>
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

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

        return entity.OrderId;
    }
}