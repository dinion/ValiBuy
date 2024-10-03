namespace Application.Common.Interfaces;

/// <summary>
/// Defines the contract for a generic repository.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation, containing the entity if found; otherwise, null.</returns>
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all entities, optionally filtered by a query.
    /// </summary>
    /// <param name="query">An optional function to customize the query.</param>
    /// <returns>A task that represents the asynchronous operation, containing a read-only list of entities.</returns>
    Task<IReadOnlyList<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? query = null);

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Removes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RemoveAsync(T entity);

    /// <summary>
    /// Removes a range of entities from the repository.
    /// </summary>
    /// <param name="entities">The entities to remove.</param>
    /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RemoveRange(IEnumerable<T> entities, CancellationToken cancellationToken);
}