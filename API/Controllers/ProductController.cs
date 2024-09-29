namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : _BaseController
{
    public ProductController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<IActionResult> CreateProduct()
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        return Ok();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetProductById([FromRoute] int id)
    {
        return Ok();
    }

    [HttpDelete]
    [Route("{id}/delete")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
        return Ok();
    }

    public async Task<IActionResult> UpdateProduct()
    {
        return Ok();
    }
}
