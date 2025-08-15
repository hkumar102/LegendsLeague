using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Queries;

/// <summary>
/// Paged search for leagues by (case-insensitive) name, optionally scoped to a series.
/// </summary>
/// <param name="Search">Substring to search in league name.</param>
/// <param name="SeriesId">Optional series scope. If null, searches across all series.</param>
/// <param name="Page">1-based page number (default 1).</param>
/// <param name="PageSize">Page size (default 20; max via validator).</param>
public sealed record SearchLeaguesQuery(
    string Search,
    Guid? SeriesId = null,
    int Page = 1,
    int PageSize = 20
) : IRequest<PaginatedResult<FantasyLeagueDto>>;
