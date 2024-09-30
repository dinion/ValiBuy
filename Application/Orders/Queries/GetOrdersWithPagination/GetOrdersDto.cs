using Application.Items.Queries.GetItems;
using Domain.Entities;

namespace Application.Orders.Queries.GetOrdersWithPagination
{
    public class GetOrdersDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<ItemDto> OrderItems { get; set; }
    }
}