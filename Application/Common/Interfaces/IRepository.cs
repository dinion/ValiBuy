namespace Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? query = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRange(IEnumerable<T> entities, CancellationToken cancellationToken);
    }
}