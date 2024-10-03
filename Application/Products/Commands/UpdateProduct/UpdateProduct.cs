using Application.Common.Interfaces;

namespace Application.Products.Commands.UpdateProduct;

/// <summary>
/// Command to update a product.
/// </summary>
public class UpdateProductCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }
}

/// <summary>
/// Handler for <see cref="UpdateProductCommand"/> to process the update request.
/// </summary>
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductCommandHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work to manage data operations.</param>
    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the updating of a product.
    /// </summary>
    /// <param name="command">The command containing product update details.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the operation.</returns>
    public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(command.Id, cancellationToken);
        Guard.Against.NotFound(command.Id, product);

        product.Name = command.Name;
        product.Price = command.Price;

        await _unitOfWork.Products.UpdateAsync(product);

        return true;
    }
}