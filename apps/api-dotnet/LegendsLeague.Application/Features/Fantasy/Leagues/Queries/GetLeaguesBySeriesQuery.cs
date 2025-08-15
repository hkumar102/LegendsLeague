using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Queries;

/// <summary>
/// Paged list of leagues scoped to a real-world series.
/// </summary>
/// <param name="SeriesId">fixtures.series id.</param>
/// <param name="Page">1-based page number (default 1).</param>
/// <param name="PageSize">Page size (default 20; max via validator).</param>
/// <param name="Sort">
/// Sort key: <c>name</c>, <c>-name</c> (default <c>name</c> asc).
/// </param>
public sealed record GetLeaguesBySeriesQuery(
    Guid SeriesId,
    int Page = 1,
    int PageSize = 20,
    string? Sort = null
) : IRequest<PaginatedResult<FantasyLeagueDto>>;
