using Application.Orders.Commands.CreateOrder;
using Application.Orders.Queries.GetCustomerOrders;

namespace API.Controllers;

/// <summary>
/// API controller for managing order-related operations.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OrderController : _BaseController
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for handling requests.</param>
    public OrderController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="order">The command containing order details.</param>
    /// <returns>The ID of the newly created order.</returns>
    /// <response code="200">Returns the order ID.</response>
    /// <response code="400">If the order creation failed.</response>
    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] CreateOrderCommand order)
    {
        var orderId = await _mediator.Send(order);
        if (orderId < 1)
            return BadRequest();

        return Ok(orderId);
    }

    /// <summary>
    /// Gets all orders for a specific customer.
    /// </summary>
    /// <param name="query">The query containing customer details.</param>
    /// <returns>A list of orders for the customer.</returns>
    /// <response code="200">Returns the list of orders.</response>
    /// <response code="400">If the customer has no orders or the query failed.</response>
    [HttpGet("all")]
    public async Task<IActionResult> GetCustomerOrders([FromQuery] GetCustomerOrdersQuery query)
    {
        var orders = await _mediator.Send(query);
        if (orders == null) return BadRequest();
        return Ok(orders);
    }
}
