using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Commands;

/// <summary>Update a fantasy team's properties.</summary>
public sealed record UpdateLeagueTeamCommand(
    Guid Id,
    string? Name,
    int? DraftPosition
) : IRequest<LeagueTeamDto>;
