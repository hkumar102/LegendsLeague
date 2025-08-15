using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LegendsLeague.Contracts.Common;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Common.Extensions;

/// <summary>
/// Helpers to turn EF Core queries into <see cref="PaginatedResult{T}"/>.
/// </summary>
public static class PaginationExtensions
{
    /// <summary>
    /// Materializes the given <paramref name="source"/> into a <see cref="PaginatedResult{TOut}"/> using the provided selector.
    /// Executes two queries: a COUNT(*) and the page query.
    /// </summary>
    public static async Task<PaginatedResult<TOut>> ToPaginatedResultAsync<TSrc, TOut>(
        this IQueryable<TSrc> source,
        int page,
        int pageSize,
        Expression<Func<TSrc, TOut>> selector,
        string? sort = null,
        CancellationToken ct = default)
    {
        var safePage = page < 1 ? 1 : page;
        var safeSize = pageSize < 1 ? 1 : pageSize;

        var total = await source.CountAsync(ct);

        var items = await source
            .Skip((safePage - 1) * safeSize)
            .Take(safeSize)
            .Select(selector)
            .ToListAsync(ct);

        return PaginatedResult<TOut>.Create(items, total, safePage, safeSize, sort);
    }

    /// <summary>
    /// Materializes the given <paramref name="source"/> into a <see cref="PaginatedResult{TOut}"/> using AutoMapper's <c>ProjectTo&lt;TOut&gt;</c>.
    /// Executes COUNT(*) against the filtered <paramref name="source"/>, then pages and projects on the server.
    /// </summary>
    public static async Task<PaginatedResult<TOut>> ToPaginatedResultAsync<TSrc, TOut>(
        this IQueryable<TSrc> source,
        int page,
        int pageSize,
        IConfigurationProvider mapperConfig,
        string? sort = null,
        CancellationToken ct = default)
    {
        var safePage = page < 1 ? 1 : page;
        var safeSize = pageSize < 1 ? 1 : pageSize;

        var total = await source.CountAsync(ct);

        var items = await source
            .Skip((safePage - 1) * safeSize)
            .Take(safeSize)
            .ProjectTo<TOut>(mapperConfig)
            .ToListAsync(ct);

        return PaginatedResult<TOut>.Create(items, total, safePage, safeSize, sort);
    }
}
