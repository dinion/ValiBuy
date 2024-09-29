namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : _BaseController
{
    public OrderController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        return Ok();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOrderById([FromRoute] int id)
    {
        return Ok();
    }

    [HttpDelete]
    [Route("{id}/delete")]
    public async Task<IActionResult> DeleteOrder([FromRoute] int id)
    {
        return Ok();
    }

    public async Task<IActionResult> UpdateOrder()
    {
        return Ok();
    }
}
