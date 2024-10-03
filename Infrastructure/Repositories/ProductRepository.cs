using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

/// <summary>
/// Repository for managing <see cref="Product"/> entities.
/// </summary>
public class ProductRepository : Repository<Product>, IProductRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepository"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public ProductRepository(ApplicationDbContext context) : base(context) { }

    /// <summary>
    /// Asynchronously retrieves a paginated list of products, ordered by name.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of products per page.</param>
    /// <returns>A <see cref="PaginatedList{Product}"/> containing the products for the specified page.</returns>
    public async Task<PaginatedList<Product>> GetProductsWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Products
            .OrderBy(x => x.Name)
            .PaginatedListAsync(pageNumber, pageSize);
    }
}