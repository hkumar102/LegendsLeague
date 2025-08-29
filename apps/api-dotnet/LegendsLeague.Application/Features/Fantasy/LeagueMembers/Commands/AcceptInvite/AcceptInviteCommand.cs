using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Commands.AcceptInvite;

/// <summary>Accept an outstanding invite for a league member.</summary>
public sealed record AcceptInviteCommand(
    Guid LeagueId,
    Guid MemberId
) : IRequest<bool>;
