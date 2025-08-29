using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Commands.ChangeRole;

/// <summary>Change a member's role in a league.</summary>
public sealed record ChangeMemberRoleCommand(
    Guid LeagueId,
    Guid MemberId,
    LeagueMemberRole NewRole
) : IRequest<bool>;
