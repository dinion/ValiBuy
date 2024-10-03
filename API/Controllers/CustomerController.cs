using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Queries.GetCustomerById;
using Application.Customers.Queries.GetCustomers;

namespace API.Controllers;

/// <summary>
/// API controller for managing customer-related operations.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CustomerController : _BaseController
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for handling requests.</param>
    public CustomerController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="command">The command containing customer details.</param>
    /// <returns>The ID of the newly created customer.</returns>
    /// <response code="200">Returns the customer ID.</response>
    /// <response code="400">If the customer creation failed.</response>
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        var customerId = await _mediator.Send(command);
        if (customerId < 1)
            return BadRequest();

        return Ok(customerId);
    }

    /// <summary>
    /// Gets a customer by ID.
    /// </summary>
    /// <param name="query">The query containing the customer ID.</param>
    /// <returns>The customer details.</returns>
    /// <response code="200">Returns the customer details.</response>
    /// <response code="400">If no customer is found for the given ID.</response>
    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerById([FromRoute] GetCustomerByIdQuery query)
    {
        var customer = await _mediator.Send(query);
        if (customer is null)
            return BadRequest();

        return Ok(customer);
    }

    /// <summary>
    /// Gets a list of all customers.
    /// </summary>
    /// <returns>A list of all customers.</returns>
    /// <response code="200">Returns the list of customers.</response>
    [HttpGet("all")]
    public async Task<IActionResult> GetCustomers()
    {
        GetCustomersQuery query = new();
        return Ok(await _mediator.Send(query));
    }
}
