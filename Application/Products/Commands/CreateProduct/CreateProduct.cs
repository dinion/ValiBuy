using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Products.Commands.CreateProduct;

/// <summary>
/// Command to create a new product.
/// </summary>
public class CreateProductCommand : IRequest<int>
{
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
/// Handler for creating a new product.
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductCommandHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work interface for managing product operations.</param>
    public CreateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the command to create a new product.
    /// </summary>
    /// <param name="command">The command containing the product information.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The unique identifier of the newly created product.</returns>
    public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var entity = new Product
        {
            Name = command.Name,
            Price = command.Price,
        };

        await _unitOfWork.Products.AddAsync(entity);

        return entity.Id;
    }
}