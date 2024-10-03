using Application.Items.Queries.GetItems;

namespace Application.Orders.Queries.GetOrdersWithPagination;

/// <summary>
/// Data transfer object representing an order with its details.
/// </summary>
public class GetOrdersDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the order.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Gets or sets the date when the order was placed.
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Gets or sets the total price of the order.
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Gets or sets the list of items included in the order.
    /// </summary>
    public IEnumerable<ItemDto> OrderItems { get; set; }
}