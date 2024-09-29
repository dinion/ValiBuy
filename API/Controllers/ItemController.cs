namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController : _BaseController
{
    public ItemController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<IActionResult> CreateItem()
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetItems()
    {
        return Ok();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetItemById([FromRoute] int id)
    {
        return Ok();
    }

    [HttpDelete]
    [Route("{id}/delete")]
    public async Task<IActionResult> DeleteItem([FromRoute] int id)
    {
        return Ok();
    }

    public async Task<IActionResult> UpdateItem()
    {
        return Ok();
    }
}
