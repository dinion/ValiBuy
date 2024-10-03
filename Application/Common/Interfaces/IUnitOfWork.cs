using Domain.Entities;

namespace Application.Common.Interfaces;

/// <summary>
/// Defines the contract for a unit of work, providing access to repositories and handling transactions.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the repository for managing <see cref="Customer"/> entities.
    /// </summary>
    IRepository<Customer> Customers { get; }

    /// <summary>
    /// Gets the repository for managing <see cref="Order"/> entities.
    /// </summary>
    IRepository<Order> Orders { get; }

    /// <summary>
    /// Gets the repository for managing <see cref="Item"/> entities.
    /// </summary>
    IRepository<Item> Items { get; }

    /// <summary>
    /// Gets the repository for managing <see cref="Product"/> entities.
    /// </summary>
    IProductRepository Products { get; }

    /// <summary>
    /// Commits all changes made in this unit of work to the underlying data store.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing the number of state entries written to the database.</returns>
    Task<int> CompleteAsync();
}