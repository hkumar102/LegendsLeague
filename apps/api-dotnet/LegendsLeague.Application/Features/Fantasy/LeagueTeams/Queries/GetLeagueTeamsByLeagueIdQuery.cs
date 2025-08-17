using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Queries;

/// <summary>List teams within a league (paginated).</summary>
public sealed record GetLeagueTeamsByLeagueIdQuery(
    Guid LeagueId,
    int Page = 1,
    int PageSize = 20,
    string? Sort = null
) : IRequest<PaginatedResult<LeagueTeamDto>>;
