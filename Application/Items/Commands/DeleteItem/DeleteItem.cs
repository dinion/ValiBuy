using Application.Common.Interfaces;

namespace Application.Items.Commands.DeleteItem;

/// <summary>
/// Represents a command to delete an item by its ID.
/// </summary>
public record DeleteItemCommand(int itemId) : IRequest;

/// <summary>
/// Handler for the <see cref="DeleteItemCommand"/>.
/// </summary>
public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteItemCommandHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work for data access.</param>
    public DeleteItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the deletion of an item by its ID.
    /// </summary>
    /// <param name="request">The request containing the ID of the item to be deleted.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Items
            .GetByIdAsync(request.itemId, cancellationToken);

        Guard.Against.NotFound(request.itemId, entity);

        await _unitOfWork.Items.RemoveAsync(entity);
    }
}