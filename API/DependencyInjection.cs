using API.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for setting up API services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// IServiceCollection extension method, to add specific API layer services.
    /// </summary>
    /// <param name="services">The service collection to which the services are added.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();

        return services;
    }
}