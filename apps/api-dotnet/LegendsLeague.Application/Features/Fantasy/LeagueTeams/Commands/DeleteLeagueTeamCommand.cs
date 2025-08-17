using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Commands;

/// <summary>Delete a fantasy team.</summary>
public sealed record DeleteLeagueTeamCommand(Guid Id) : IRequest<bool>;
