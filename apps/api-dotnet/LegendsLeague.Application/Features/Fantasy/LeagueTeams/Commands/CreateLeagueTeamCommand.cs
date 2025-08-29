using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Commands;

/// <summary>Create a fantasy team within a league.</summary>
public sealed record CreateLeagueTeamCommand(
    Guid LeagueId,
    string Name,
    Guid OwnerUserId
) : IRequest<LeagueTeamDto>;
