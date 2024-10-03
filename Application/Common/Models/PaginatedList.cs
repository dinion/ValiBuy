namespace Application.Common.Models;

/// <summary>
/// Represents a paginated list of items.
/// </summary>
/// <typeparam name="T">The type of items in the list.</typeparam>
public class PaginatedList<T>
{
    /// <summary>
    /// Gets the items in the current page.
    /// </summary>
    public IReadOnlyCollection<T> Items { get; }

    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Gets the total number of items in the entire collection.
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
    /// </summary>
    /// <param name="items">The items in the current page.</param>
    /// <param name="count">The total number of items in the collection.</param>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The size of each page.</param>
    public PaginatedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    /// <summary>
    /// Gets a value indicating whether there is a previous page.
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Gets a value indicating whether there is a next page.
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Creates a <see cref="PaginatedList{T}"/> asynchronously from a source queryable.
    /// </summary>
    /// <param name="source">The queryable source of items.</param>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The size of each page.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="PaginatedList{T}"/> as the result.</returns>
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}