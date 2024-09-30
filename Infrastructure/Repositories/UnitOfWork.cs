using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IRepository<Customer> _customers;
        private IRepository<Order> _orders;
        private IRepository<Item> _items;
        private IProductRepository _products;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Customer> Customers => _customers ??= new Repository<Customer>(_context);
        public IRepository<Order> Orders => _orders ??= new Repository<Order>(_context);
        public IRepository<Item> Items => _items ??= new Repository<Item>(_context);
        public IProductRepository Products => _products ??= new ProductRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}