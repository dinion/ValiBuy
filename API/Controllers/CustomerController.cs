using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Queries.GetCustomerById;
using Application.Customers.Queries.GetCustomers;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : _BaseController
{
    public CustomerController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        var customerId = await _mediator.Send(command);
        if (customerId < 1)
            return BadRequest();

        return Ok(customerId);
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerById([FromRoute] GetCustomerByIdQuery query)
    {
        var customer = await _mediator.Send(query);
        if (customer is null)
            return BadRequest();

        return Ok(customer);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetCustomers()
    {
        GetCustomersQuery query = new();
        return Ok(await _mediator.Send(query));
    }

}
