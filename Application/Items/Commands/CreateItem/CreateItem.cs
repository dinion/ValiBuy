using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Items.Commands.CreateItem;

public record CreateItemCommand : IRequest<int>
{
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
        var entity = new Item
        {
            OrderId = 1,
            ProductId = 1,
            Quantity = 1
        };

        await _unitOfWork.Items.AddAsync(entity);

        return entity.OrderId;
    }
}