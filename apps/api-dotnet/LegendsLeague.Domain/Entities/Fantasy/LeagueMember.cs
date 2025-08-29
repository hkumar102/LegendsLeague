using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// Membership record linking a user to a fantasy league.
/// </summary>
public sealed class LeagueMember : SoftDeletableEntity
{
    public Guid Id { get; set; }

    /// <summary>FK → fantasy.leagues.id.</summary>
    public Guid LeagueId { get; set; }

    /// <summary>External user id (from your auth/user system).</summary>
    public Guid UserId { get; set; }

    /// <summary>Member role (Commissioner/Member).</summary>
    public LeagueMemberRole Role { get; set; } = LeagueMemberRole.Member;
   public LeagueMemberStatus Status { get; set; } = LeagueMemberStatus.Invited;

    public DateTimeOffset? InvitedAtUtc { get; set; }
    public DateTimeOffset? JoinedAtUtc { get; set; }

    // Navs
    public FantasyLeague League { get; set; } = default!;
}
