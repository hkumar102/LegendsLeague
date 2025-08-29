namespace LegendsLeague.Contracts.Fantasy;

/// <summary>Request to change a member's role.</summary>
public sealed class ChangeMemberRoleRequest
{
    public LeagueMemberRole Role { get; init; }
}
