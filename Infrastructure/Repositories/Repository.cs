using Application.Common.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// A generic repository class for managing entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of entity being managed by the repository.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The entity with the specified identifier, or <c>null</c> if not found.</returns>
        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Asynchronously retrieves all entities, optionally applying a query.
        /// </summary>
        /// <param name="query">An optional function to apply to the query.</param>
        /// <returns>A list of entities of type <typeparamref name="T"/>.</returns>
        public async Task<IReadOnlyList<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? query = null)
        {
            IQueryable<T> queryable = _context.Set<T>();

            if (query != null)
            {
                queryable = query(queryable);
            }

            return await queryable.ToListAsync();
        }

        /// <summary>
        /// Asynchronously adds a new entity to the context.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously updates an existing entity in the context.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously removes an entity from the context.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously removes a range of entities from the context.
        /// </summary>
        /// <param name="entities">The entities to remove.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task RemoveRange(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            _context.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
