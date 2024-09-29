using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Items.Commands.DeleteItem;

public record DeleteItemCommand(int itemId) : IRequest;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteItemCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTodoItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Items
            .GetByIdAsync(request.itemId, cancellationToken);

        Guard.Against.NotFound(request.itemId, entity);

        await _unitOfWork.Items.DeleteAsync(entity);
    }
}
