using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetProductsWithPagination;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : _BaseController
{
    public ProductController(IMediator mediator) : base(mediator) { }

    [HttpPost("create-product")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var productId = await _mediator.Send(command);
        if (productId < 1)
            return BadRequest();

        return Ok();
    }

    [HttpPost("{productId}/update-product")]
    public async Task<IActionResult> UpdateProduct([FromRoute] int productId, UpdateProductCommand command)
    {
        var isSuccess = await _mediator.Send(command);
        if (isSuccess != true)
            return BadRequest();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts([FromQuery] GetProductsWithPaginationQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveProductById([FromQuery] DeleteProductCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
