using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Queries.GetMemberById;

/// <summary>Get a league member by id.</summary>
public sealed record GetLeagueMemberByIdQuery(Guid MemberId) : IRequest<LeagueMemberDto?>;
