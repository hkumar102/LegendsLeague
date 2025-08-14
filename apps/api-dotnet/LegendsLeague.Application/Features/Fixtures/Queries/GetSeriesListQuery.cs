using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Contracts.Series;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Queries;

/// <summary>
/// Query to retrieve a paged, filtered, and sorted list of cricket series.
/// </summary>
/// <param name="SeasonYear">Optional exact season year filter (e.g., 2026).</param>
/// <param name="Search">Optional case-insensitive name substring filter.</param>
/// <param name="Page">1-based page number (default 1).</param>
/// <param name="PageSize">Page size (default 20; max enforced by validator).</param>
/// <param name="Sort">
/// Sort key: one of <c>name</c>, <c>-name</c>, <c>seasonYear</c>, <c>-seasonYear</c>.
/// A leading '-' means descending.
/// </param>
public sealed record GetSeriesListQuery(
    int? SeasonYear = null,
    string? Search = null,
    int Page = 1,
    int PageSize = 20,
    string? Sort = null
) : IRequest<IReadOnlyList<SeriesDto>>;

/// <summary>
/// Handles <see cref="GetSeriesListQuery"/> using the <see cref="IFixturesDbContext"/> read surface.
/// </summary>
public sealed class GetSeriesListQueryHandler : IRequestHandler<GetSeriesListQuery, IReadOnlyList<SeriesDto>>
{
    private readonly IFixturesDbContext _db;

    /// <summary>
    /// Initializes a new instance of the handler.
    /// </summary>
    /// <param name="db">Fixtures read/write abstraction.</param>
    public GetSeriesListQueryHandler(IFixturesDbContext db) => _db = db;

    /// <inheritdoc />
    public async Task<IReadOnlyList<SeriesDto>> Handle(GetSeriesListQuery request, CancellationToken ct)
    {
        IQueryable<LegendsLeague.Domain.Entities.Fixtures.Series> q = _db.Series.AsNoTracking();

        if (request.SeasonYear.HasValue)
        {
            q = q.Where(s => s.SeasonYear == request.SeasonYear.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim().ToLower();
            q = q.Where(s => EF.Functions.ILike(s.Name, $"%{term}%"));
        }

        // Sorting
        q = request.Sort?.Trim() switch
        {
            "name"          => q.OrderBy(s => s.Name).ThenBy(s => s.SeasonYear),
            "-name"         => q.OrderByDescending(s => s.Name).ThenByDescending(s => s.SeasonYear),
            "seasonYear"    => q.OrderBy(s => s.SeasonYear).ThenBy(s => s.Name),
            "-seasonYear"   => q.OrderByDescending(s => s.SeasonYear).ThenByDescending(s => s.Name),
            _               => q.OrderBy(s => s.SeasonYear).ThenBy(s => s.Name)
        };

        // Paging (Page is 1-based)
        var skip = Math.Max(0, (request.Page - 1) * request.PageSize);
        q = q.Skip(skip).Take(request.PageSize);

        // Projection to contracts
        var rows = await q
            .Select(s => new SeriesDto(s.Id, s.Name, s.SeasonYear))
            .ToListAsync(ct);

        return rows;
    }
}
