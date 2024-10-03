using Application.Common.Interfaces;

namespace Application.Products.Commands.PurgeProduct;

/// <summary>
/// Command to purge all products from the repository.
/// </summary>
public record PurgeProductCommand : IRequest;

/// <summary>
/// Handler for <see cref="PurgeProductCommand"/> to process the purging of products.
/// </summary>
public class PurgeProductCommandHandler : IRequestHandler<PurgeProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="PurgeProductCommandHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work instance to access the product repository.</param>
    public PurgeProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the purging of all products from the repository.
    /// </summary>
    /// <param name="request">The command containing the purge request.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public async Task Handle(PurgeProductCommand request, CancellationToken cancellationToken)
    {
        // Retrieve all products and remove them
        await _unitOfWork.Products.RemoveRange(await _unitOfWork.Products.GetAllAsync(), cancellationToken);
    }
}