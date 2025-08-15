namespace LegendsLeague.Contracts.Common;

/// <summary>
/// Standardized pagination envelope returned by list endpoints.
/// </summary>
public sealed class PaginatedResult<T>
{
    /// <summary>The 1-based page index that was requested.</summary>
    public int Page { get; init; }

    /// <summary>The page size that was requested.</summary>
    public int PageSize { get; init; }

    /// <summary>Total number of items across all pages (server-side count).</summary>
    public int TotalCount { get; init; }

    /// <summary>Total number of pages given <see cref="TotalCount"/> and <see cref="PageSize"/>.</summary>
    public int TotalPages { get; init; }

    /// <summary>Optional sort key used by the server (for client echoing).</summary>
    public string? Sort { get; init; }

    /// <summary>The items for the current page.</summary>
    public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();

    public bool HasPrevious => Page > 1;
    public bool HasNext => Page < TotalPages;

    /// <summary>Create an instance from raw values; <paramref name="totalCount"/> is not capped by page size.</summary>
    public static PaginatedResult<T> Create(IReadOnlyList<T> items, int totalCount, int page, int pageSize, string? sort = null)
    {
        var safePage = page < 1 ? 1 : page;
        var safePageSize = pageSize < 1 ? 1 : pageSize;
        var totalPages = (int)Math.Ceiling(totalCount / (double)safePageSize);

        return new PaginatedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            Page = safePage,
            PageSize = safePageSize,
            TotalPages = totalPages,
            Sort = sort
        };
    }
}
