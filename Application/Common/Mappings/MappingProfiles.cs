using Application.Common.Models;
using Application.Customers.Queries.GetCustomerById;
using Application.Products.Queries.GetProductsWithPagination;
using Domain.Entities;

namespace Application.Common.Mappings;

/// <summary>
/// Defines AutoMapper profiles for mapping between domain entities and data transfer objects (DTOs).
/// </summary>
public class MappingProfiles : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfiles"/> class.
    /// </summary>
    public MappingProfiles()
    {
        CreateMap<Customer, CustomerDto>();

        CreateMap<PaginatedList<Customer>, PaginatedList<CustomerDto>>()
            .ConvertUsing((source, destination, context) =>
            {
                var mappedItems = context.Mapper.Map<List<CustomerDto>>(source.Items);
                return new PaginatedList<CustomerDto>(mappedItems, source.TotalCount, source.PageNumber, source.TotalPages);
            });

        CreateMap<Product, ProductDto>();

        CreateMap<PaginatedList<Product>, PaginatedList<ProductDto>>()
            .ConvertUsing((source, destination, context) =>
            {
                var mappedItems = context.Mapper.Map<List<ProductDto>>(source.Items);
                return new PaginatedList<ProductDto>(mappedItems, source.TotalCount, source.PageNumber, source.TotalPages);
            });
    }
}