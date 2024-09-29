using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        private IRepository<Customer> _customers;
        private IRepository<Order> _orders;
        private IRepository<Item> _items;
        private IRepository<Product> _products;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IRepository<Customer> Customers => _customers ??= new Repository<Customer>(_context);
        public IRepository<Order> Orders => _orders ??= new Repository<Order>(_context);
        public IRepository<Item> Items => _items ??= new Repository<Item>(_context);
        public IRepository<Product> Products => _products ??= new Repository<Product>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}