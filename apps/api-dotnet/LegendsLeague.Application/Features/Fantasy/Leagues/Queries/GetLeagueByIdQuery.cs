using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Queries;

/// <summary>
/// Query to fetch a fantasy league by its identifier.
/// </summary>
/// <param name="Id">League id.</param>
public sealed record GetLeagueByIdQuery(Guid Id) : IRequest<FantasyLeagueDto?>;
