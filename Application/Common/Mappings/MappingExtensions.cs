using Application.Common.Models;
using AutoMapper.QueryableExtensions;

namespace Application.Common.Mappings;

/// <summary>
/// Provides extension methods for IQueryable to facilitate mapping and pagination.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Asynchronously creates a paginated list of the specified type.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination objects.</typeparam>
    /// <param name="queryable">The queryable source to paginate.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items to include on each page.</param>
    /// <returns>A task that represents the asynchronous operation, containing a paginated list of <typeparamref name="TDestination"/>.</returns>
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

    /// <summary>
    /// Asynchronously projects the queryable source into a list of the specified type.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination objects.</typeparam>
    /// <param name="queryable">The queryable source to project.</param>
    /// <param name="configuration">The AutoMapper configuration provider.</param>
    /// <returns>A task that represents the asynchronous operation, containing a list of <typeparamref name="TDestination"/>.</returns>
    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
}
