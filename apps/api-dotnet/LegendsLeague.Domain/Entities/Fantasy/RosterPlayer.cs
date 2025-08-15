using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// A mapping of a real player into a fantasy team roster with a slot and active window.
/// </summary>
public sealed class RosterPlayer : SoftDeletableEntity
{
    public Guid Id { get; set; }

    /// <summary>FK → fantasy.league_teams.id.</summary>
    public Guid LeagueTeamId { get; set; }

    /// <summary>FK → fixtures.players.id.</summary>
    public Guid PlayerId { get; set; }

    /// <summary>Slot assignment (BAT/BWL/AR/WK/BENCH).</summary>
    public RosterSlot Slot { get; set; } = RosterSlot.BENCH;

    /// <summary>When the player became active in this slot (UTC).</summary>
    public DateTimeOffset? ActiveFromUtc { get; set; }

    /// <summary>When the player stopped being active in this slot (UTC).</summary>
    public DateTimeOffset? ActiveToUtc { get; set; }

    // Navs
    public LeagueTeam LeagueTeam { get; set; } = default!;
}
