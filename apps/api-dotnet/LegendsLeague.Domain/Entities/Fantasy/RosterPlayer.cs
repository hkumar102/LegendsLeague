using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// A player on a fantasy team's roster (current or historical window).
/// Cross-context PlayerId is a scalar (player lives in Fixtures context).
/// </summary>
public class RosterPlayer : AuditableEntity
{
    public Guid Id { get; set; }

    public Guid LeagueTeamId { get; set; }
    public LeagueTeam LeagueTeam { get; set; } = default!;

    public Guid PlayerId { get; set; } // Fixtures.Player (scalar FK across contexts)

    public RosterSlot Slot { get; set; }
    public RosterStatus Status { get; set; } = RosterStatus.Active;

    /// <summary>When this roster assignment became effective (inclusive).</summary>
    public DateTimeOffset EffectiveFromUtc { get; set; }

    /// <summary>When this roster assignment ended (exclusive). Null means active.</summary>
    public DateTimeOffset? EffectiveToUtc { get; set; }
}
