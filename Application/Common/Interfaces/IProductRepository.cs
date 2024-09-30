using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PaginatedList<Product>> GetProductsWithPagination(int pageNumber, int pageSize);
    }
}