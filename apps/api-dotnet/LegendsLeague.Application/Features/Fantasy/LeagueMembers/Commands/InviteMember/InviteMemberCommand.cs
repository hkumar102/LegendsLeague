using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Commands.InviteMember;

/// <summary>Invite a user to a fantasy league with a role.</summary>
public sealed record InviteMemberCommand(
    Guid LeagueId,
    Guid UserId,
    LeagueMemberRole Role
) : IRequest<LeagueMemberDto>;
