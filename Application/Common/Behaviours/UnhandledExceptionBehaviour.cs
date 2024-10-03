using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

/// <summary>
/// Middleware that logs unhandled exceptions for requests in the pipeline.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnhandledExceptionBehaviour{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="logger">The logger to use for logging unhandled exceptions.</param>
    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handles the request and logs any unhandled exceptions that occur during processing.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="next">The delegate to invoke the next behavior in the pipeline.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the response.</returns>
    /// <exception cref="Exception">Throws the caught exception to be handled further up the pipeline.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "ValiBuy Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}