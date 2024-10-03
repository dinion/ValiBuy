using Domain.Entities;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// Defines the contract for the application database context.
    /// </summary>
    public interface IApplicationDbContext
    {
        public DbSet<Customer> Customers { get; }
        public DbSet<Order> Orders { get; }
        public DbSet<Item> Items { get; }
        public DbSet<Product> Products { get; }

        /// <summary>
        /// Saves changes made in the context to the database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}