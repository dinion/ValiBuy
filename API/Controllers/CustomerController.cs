namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : _BaseController
{
    public CustomerController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer()
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        return Ok();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetCustomerById([FromRoute] int id)
    {
        return Ok();
    }

    [HttpDelete]
    [Route("{id}/delete")]
    public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
    {
        return Ok();
    }

    public async Task<IActionResult> UpdateCustomer()
    {
        return Ok();
    }
}
