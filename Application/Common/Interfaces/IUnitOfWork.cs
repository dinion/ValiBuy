using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Customer> Customers { get; }
        IRepository<Order> Orders { get; }
        IRepository<Item> Items { get; }
        IProductRepository Products { get; }

        Task<int> CompleteAsync();
    }
}