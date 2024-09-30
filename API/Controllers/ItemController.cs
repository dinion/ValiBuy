using Application.Items.Commands.CreateItem;
using System.Runtime.CompilerServices;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController : _BaseController
{
    public ItemController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<IActionResult> CreateItem(CreateItemCommand command)
    {
        var itemId = await _mediator.Send(command);
        if (itemId < 1)
            return BadRequest();

        return Ok(itemId);
    }

}
