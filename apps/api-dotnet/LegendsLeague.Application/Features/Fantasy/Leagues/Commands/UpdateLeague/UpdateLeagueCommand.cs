using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Commands.UpdateLeague;

public sealed record UpdateLeagueCommand(
    Guid Id,
    string? Name,
    int? MaxTeams
) : IRequest<FantasyLeagueDto>;
