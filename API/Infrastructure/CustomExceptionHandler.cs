using Application.Common.Exceptions;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Diagnostics;

namespace API.Infrastructure;

/// <summary>
/// Custom exception handler for managing specific application exceptions.
/// </summary>
public class CustomExceptionHandler : IExceptionHandler
{
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomExceptionHandler"/> class.
    /// </summary>
    public CustomExceptionHandler()
    {
        _exceptionHandlers = new()
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
        };
    }

    /// <summary>
    /// Attempts to handle an exception asynchronously.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="exception">The exception to handle.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> that returns true if the exception was handled, false otherwise.</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();

        if (_exceptionHandlers.ContainsKey(exceptionType))
        {
            await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Handles a <see cref="ValidationException"/> by returning a 400 Bad Request response with validation details.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="ex">The exception being handled.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task HandleValidationException(HttpContext httpContext, Exception ex)
    {
        var exception = (ValidationException)ex;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        await httpContext.Response.WriteAsJsonAsync(new ValidationProblemDetails(exception.Errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        });
    }

    /// <summary>
    /// Handles a <see cref="NotFoundException"/> by returning a 404 Not Found response with error details.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="ex">The exception being handled.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task HandleNotFoundException(HttpContext httpContext, Exception ex)
    {
        var exception = (NotFoundException)ex;

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
        {
            Status = StatusCodes.Status404NotFound,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message
        });
    }
}
