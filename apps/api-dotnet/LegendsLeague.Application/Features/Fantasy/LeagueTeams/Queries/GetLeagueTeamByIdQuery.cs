using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Queries;

/// <summary>Get a team by id.</summary>
public sealed record GetLeagueTeamByIdQuery(Guid Id) : IRequest<LeagueTeamDto?>;
