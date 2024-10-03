using Application.Items.Commands.CreateItem;
using System.Runtime.CompilerServices;

namespace API.Controllers;

/// <summary>
/// API controller for managing item-related operations.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ItemController : _BaseController
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for handling requests.</param>
    public ItemController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Creates a new item.
    /// </summary>
    /// <param name="command">The command containing item details.</param>
    /// <returns>The ID of the newly created item.</returns>
    /// <response code="200">Returns the item ID.</response>
    /// <response code="400">If the item creation failed.</response>
    [HttpPost]
    public async Task<IActionResult> CreateItem([FromBody] CreateItemCommand command)
    {
        var itemId = await _mediator.Send(command);
        if (itemId < 1)
            return BadRequest();

        return Ok(itemId);
    }
}
