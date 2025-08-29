namespace LegendsLeague.Contracts.Fantasy;

/// <summary>Request to invite a user into a league.</summary>
public sealed class InviteLeagueMemberRequest
{
    public Guid UserId { get; init; }
    public LeagueMemberRole Role { get; init; } = LeagueMemberRole.Member;
}
