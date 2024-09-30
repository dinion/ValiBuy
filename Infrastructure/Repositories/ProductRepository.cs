using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PaginatedList<Product>> GetProductsWithPagination(int pageNumber, int pageSize)
        {
            return await _context.Products
                .OrderBy(x => x.Name)
                .PaginatedListAsync(pageNumber, pageSize);
        }
    }
}