using Application.Common.Interfaces;

namespace Application.Products.Commands.DeleteProduct;

/// <summary>
/// Command to delete a product by its ID.
/// </summary>
public record DeleteProductCommand(int ProductId) : IRequest;

/// <summary>
/// Handler for <see cref="DeleteProductCommand"/> to process the deletion of a product.
/// </summary>
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductCommandHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work instance to access the product repository.</param>
    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the deletion of the product identified by the command.
    /// </summary>
    /// <param name="command">The command containing the product ID to be deleted.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(command.ProductId, cancellationToken);

        // Guard against not found product
        Guard.Against.NotFound(command.ProductId, product);

        await _unitOfWork.Products.RemoveAsync(product);
    }
}