using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

/// <summary>
/// Unit of Work implementation that provides repositories for managing entities.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private IRepository<Customer> _customers;
    private IRepository<Order> _orders;
    private IRepository<Item> _items;
    private IProductRepository _products;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }


    /// <summary>
    /// Gets the repository for managing <see cref="Customer"/> entities.
    /// </summary>
    public IRepository<Customer> Customers => _customers ??= new Repository<Customer>(_context);

    /// <summary>
    /// Gets the repository for managing <see cref="Order"/> entities.
    /// </summary>
    public IRepository<Order> Orders => _orders ??= new Repository<Order>(_context);

    /// <summary>
    /// Gets the repository for managing <see cref="Item"/> entities.
    /// </summary>
    public IRepository<Item> Items => _items ??= new Repository<Item>(_context);

    /// <summary>
    /// Gets the repository for managing <see cref="Product"/> entities.
    /// </summary>
    public IProductRepository Products => _products ??= new ProductRepository(_context);

    /// <summary>
    /// Saves changes made in the context to the database asynchronously.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}