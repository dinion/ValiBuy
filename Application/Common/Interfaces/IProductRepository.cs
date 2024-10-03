using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// Defines the contract for the product repository.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of products per page.</param>
        /// <returns>A task that represents the asynchronous operation, containing a <see cref="PaginatedList{Product}"/> of products.</returns>
        Task<PaginatedList<Product>> GetProductsWithPagination(int pageNumber, int pageSize);
    }
}