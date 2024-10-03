using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetProductsWithPagination;

namespace API.Controllers;

/// <summary>
/// API controller for managing product-related operations.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductController : _BaseController
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for handling requests.</param>
    public ProductController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="command">The command containing product details.</param>
    /// <returns>Returns an OK result if the product is successfully created.</returns>
    /// <response code="200">If the product was successfully created.</response>
    /// <response code="400">If the product creation failed.</response>
    [HttpPost("create-product")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var productId = await _mediator.Send(command);
        if (productId < 1)
            return BadRequest();

        return Ok();
    }

    /// <summary>
    /// Updates an existing product by its ID.
    /// </summary>
    /// <param name="command">The command containing updated product details.</param>
    /// <returns>Returns an OK result if the product is successfully updated.</returns>
    /// <response code="200">If the product was successfully updated.</response>
    /// <response code="400">If the product update failed.</response>
    [HttpPost("{productId}/update-product")]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
    {
        var isSuccess = await _mediator.Send(command);
        if (!isSuccess)
            return BadRequest();

        return Ok();
    }

    /// <summary>
    /// Gets all products with pagination.
    /// </summary>
    /// <param name="query">The query containing pagination details.</param>
    /// <returns>Returns a paginated list of products.</returns>
    /// <response code="200">Returns the list of products.</response>
    [HttpGet]
    public async Task<IActionResult> GetAllProducts([FromQuery] GetProductsWithPaginationQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    /// <summary>
    /// Removes a product by its ID.
    /// </summary>
    /// <param name="command">The command containing the product ID to delete.</param>
    /// <returns>Returns an OK result if the product is successfully removed.</returns>
    /// <response code="200">If the product was successfully removed.</response>
    [HttpPost("remove")]
    public async Task<IActionResult> RemoveProductById([FromQuery] DeleteProductCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
