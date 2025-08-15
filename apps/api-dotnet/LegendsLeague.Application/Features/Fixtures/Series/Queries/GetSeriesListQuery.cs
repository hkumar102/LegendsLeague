using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Contracts.Series;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Series.Queries;

/// <summary>
/// Query to retrieve a paged, filtered, and sorted list of cricket series.
/// </summary>
/// <param name="SeasonYear">Optional exact season year filter (e.g., 2026).</param>
/// <param name="Search">Optional case-insensitive name substring filter.</param>
/// <param name="Page">1-based page number (default 1).</param>
/// <param name="PageSize">Page size (default 20; max enforced by validator).</param>
/// <param name="Sort">Sort key: name, -name, seasonYear, -seasonYear.</param>
public sealed record GetSeriesListQuery(
    int? SeasonYear = null,
    string? Search = null,
    int Page = 1,
    int PageSize = 20,
    string? Sort = null
) : IRequest<IReadOnlyList<SeriesDto>>;

public sealed class GetSeriesListQueryHandler : IRequestHandler<GetSeriesListQuery, IReadOnlyList<SeriesDto>>
{
    private readonly IFixturesDbContext _db;

    public GetSeriesListQueryHandler(IFixturesDbContext db) => _db = db;

    public async Task<IReadOnlyList<SeriesDto>> Handle(GetSeriesListQuery request, CancellationToken ct)
    {
        IQueryable<LegendsLeague.Domain.Entities.Fixtures.Series> q = _db.Series.AsNoTracking();

        if (request.SeasonYear.HasValue)
            q = q.Where(s => s.SeasonYear == request.SeasonYear.Value);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim().ToLower();
            q = q.Where(s => EF.Functions.ILike(s.Name, $"%{term}%"));
        }

        q = request.Sort?.Trim() switch
        {
            "name"        => q.OrderBy(s => s.Name).ThenBy(s => s.SeasonYear),
            "-name"       => q.OrderByDescending(s => s.Name).ThenByDescending(s => s.SeasonYear),
            "seasonYear"  => q.OrderBy(s => s.SeasonYear).ThenBy(s => s.Name),
            "-seasonYear" => q.OrderByDescending(s => s.SeasonYear).ThenByDescending(s => s.Name),
            _             => q.OrderBy(s => s.SeasonYear).ThenBy(s => s.Name)
        };

        var skip = Math.Max(0, (request.Page - 1) * request.PageSize);
        q = q.Skip(skip).Take(request.PageSize);

        return await q
            .Select(s => new SeriesDto(s.Id, s.Name, s.SeasonYear))
            .ToListAsync(ct);
    }
}
