using Application.Orders.Commands.CreateOrder;
using Application.Orders.Queries.GetCustomerOrders;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : _BaseController
{
    public OrderController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] CreateOrderCommand order)
    {
        var orderId = await _mediator.Send(order);
        if (orderId < 1)
            return BadRequest();

        return Ok(orderId);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetCustomerOrders([FromQuery] GetCustomerOrdersQuery query)
    {
        var orders = await _mediator.Send(query);
        if (orders == null) return BadRequest();
        return Ok(orders);
    }
}
