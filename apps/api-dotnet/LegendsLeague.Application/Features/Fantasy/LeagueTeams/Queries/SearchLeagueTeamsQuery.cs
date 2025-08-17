using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Queries;

/// <summary>Search teams by name (case-insensitive), optional league scope.</summary>
public sealed record SearchLeagueTeamsQuery(
    string Search,
    Guid? LeagueId = null,
    int Page = 1,
    int PageSize = 20
) : IRequest<PaginatedResult<LeagueTeamDto>>;
