using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Commands.RemoveMember;

/// <summary>Remove a member from a league.</summary>
public sealed record RemoveMemberCommand(
    Guid LeagueId,
    Guid MemberId
) : IRequest<bool>;
