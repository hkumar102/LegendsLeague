using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Commands.CreateLeague;

public sealed record CreateLeagueCommand(
    Guid SeriesId,
    string Name,
    int MaxTeams,
    Guid CommissionerUserId
) : IRequest<FantasyLeagueDto>;
