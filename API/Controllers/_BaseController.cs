namespace API.Controllers
{
    /// <summary>
    /// Base controller providing shared functionality for all API controllers.
    /// </summary>
    public class _BaseController : ControllerBase
    {
        /// <summary>
        /// The mediator instance used for sending requests and receiving responses in the application.
        /// </summary>
        public readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="_BaseController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator used to handle requests and responses across different parts of the application.</param>
        /// <exception cref="ArgumentNullException">Thrown when the mediator instance is null.</exception>
        public _BaseController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    }
}