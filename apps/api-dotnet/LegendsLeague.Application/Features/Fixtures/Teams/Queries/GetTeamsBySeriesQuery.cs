using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Teams;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Queries;

/// <summary>
/// Query to retrieve a paged, filtered, and sorted list of real teams within a specific series.
/// </summary>
/// <param name="SeriesId">Required series identifier.</param>
/// <param name="Search">Optional case-insensitive substring to match against team name or short name.</param>
/// <param name="Page">1-based page number (default 1).</param>
/// <param name="PageSize">Page size (default 20; max validated by <see cref="Validators.GetTeamsBySeriesQueryValidator"/>).</param>
/// <param name="Sort">Sort key: <c>name</c>, <c>-name</c>, <c>shortName</c>, <c>-shortName</c> (default: <c>name</c> asc).</param>
public sealed record GetTeamsBySeriesQuery(
    Guid SeriesId,
    string? Search = null,
    int Page = 1,
    int PageSize = 20,
    string? Sort = null
) : IRequest<PaginatedResult<RealTeamDto>>;

/// <summary>
/// Handles <see cref="GetTeamsBySeriesQuery"/> using the fixtures read surface.
/// </summary>
public sealed class GetTeamsBySeriesQueryHandler
    : IRequestHandler<GetTeamsBySeriesQuery, PaginatedResult<RealTeamDto>>
{
    private readonly IFixturesDbContext _db;

    /// <summary>
    /// Initializes a new handler instance.
    /// </summary>
    /// <param name="db">Fixtures persistence abstraction.</param>
    public GetTeamsBySeriesQueryHandler(IFixturesDbContext db) => _db = db;

    /// <inheritdoc />
    public async Task<PaginatedResult<RealTeamDto>> Handle(GetTeamsBySeriesQuery request, CancellationToken ct)
    {
        // Base filter: teams for the given series (soft-deleted rows are excluded by the global filter).
        var q = _db.RealTeams
            .AsNoTracking()
            .Where(t => t.SeriesId == request.SeriesId);

        // Optional search (Postgres ILIKE via Npgsql, falls back appropriately in tests with InMemory)
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim().ToLower();
            q = q.Where(t =>
                EF.Functions.ILike(t.Name, $"%{term}%") ||
                (t.ShortName != null && EF.Functions.ILike(t.ShortName, $"%{term}%")));
        }

        // Sorting
        q = request.Sort?.Trim() switch
        {
            "name"        => q.OrderBy(t => t.Name),
            "-name"       => q.OrderByDescending(t => t.Name),
            "shortName"   => q.OrderBy(t => t.ShortName).ThenBy(t => t.Name),
            "-shortName"  => q.OrderByDescending(t => t.ShortName).ThenByDescending(t => t.Name),
            _             => q.OrderBy(t => t.Name)
        };

        // Page and project → DTO inside a PaginatedResult envelope
        return await q.ToPaginatedResultAsync(
            page: request.Page,
            pageSize: request.PageSize,
            selector: t => new RealTeamDto(t.Id, t.SeriesId, t.Name, t.ShortName),
            sort: request.Sort,
            ct: ct);
    }
}
