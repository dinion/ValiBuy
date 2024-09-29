namespace API.Controllers;

public class _BaseController : ControllerBase
{
    public readonly IMediator _mediator;

    public _BaseController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
}