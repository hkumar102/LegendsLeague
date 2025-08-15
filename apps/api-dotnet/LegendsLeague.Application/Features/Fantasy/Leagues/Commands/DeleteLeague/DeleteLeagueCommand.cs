using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Commands.DeleteLeague;

public sealed record DeleteLeagueCommand(Guid Id) : IRequest<bool>;
