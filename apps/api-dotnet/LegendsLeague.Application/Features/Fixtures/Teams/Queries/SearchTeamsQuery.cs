using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Teams;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Queries;

/// <summary>
/// Global search for real teams, optionally restricted to a series.
/// </summary>
/// <param name="SeriesId">Optional series filter; when provided, limits to that series.</param>
/// <param name="Search">Optional case-insensitive substring matched against name or short name.</param>
/// <param name="Page">1-based page number (default 1).</param>
/// <param name="PageSize">Page size (default 20; max validated by validator).</param>
/// <param name="Sort">Sort key: name, -name, shortName, -shortName (default name asc).</param>
public sealed record SearchTeamsQuery(
    Guid? SeriesId = null,
    string? Search = null,
    int Page = 1,
    int PageSize = 20,
    string? Sort = null
) : IRequest<PaginatedResult<RealTeamDto>>;

/// <summary>
/// Handles <see cref="SearchTeamsQuery"/> using the fixtures read surface.
/// </summary>
public sealed class SearchTeamsQueryHandler : IRequestHandler<SearchTeamsQuery, PaginatedResult<RealTeamDto>>
{
    private readonly IFixturesDbContext _db;

    /// <summary>Initializes handler.</summary>
    public SearchTeamsQueryHandler(IFixturesDbContext db) => _db = db;

    /// <inheritdoc />
    public async Task<PaginatedResult<RealTeamDto>> Handle(SearchTeamsQuery request, CancellationToken ct)
    {
        var q = _db.RealTeams.AsNoTracking().AsQueryable();

        if (request.SeriesId.HasValue)
        {
            q = q.Where(t => t.SeriesId == request.SeriesId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim().ToLower();
            q = q.Where(t =>
                EF.Functions.ILike(t.Name, $"%{term}%") ||
                (t.ShortName != null && EF.Functions.ILike(t.ShortName, $"%{term}%")));
        }

        q = request.Sort?.Trim() switch
        {
            "name"        => q.OrderBy(t => t.Name),
            "-name"       => q.OrderByDescending(t => t.Name),
            "shortName"   => q.OrderBy(t => t.ShortName).ThenBy(t => t.Name),
            "-shortName"  => q.OrderByDescending(t => t.ShortName).ThenByDescending(t => t.Name),
            _             => q.OrderBy(t => t.Name)
        };

        return await q.ToPaginatedResultAsync(
            request.Page,
            request.PageSize,
            t => new RealTeamDto(t.Id, t.SeriesId, t.Name, t.ShortName),
            request.Sort,
            ct);
    }
}
