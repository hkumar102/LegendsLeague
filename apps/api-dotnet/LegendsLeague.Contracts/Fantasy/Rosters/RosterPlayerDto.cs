using LegendsLeague.Contracts.Fantasy;

namespace LegendsLeague.Contracts.Fantasy.Rosters;

/// <summary>
/// A player currently (or historically) on a fantasy team's roster.
/// </summary>
public sealed class RosterPlayerDto
{
    public Guid Id { get; set; }
    public Guid LeagueTeamId { get; set; }
    public Guid PlayerId { get; set; }            // from Fixtures context (scalar)
    public RosterSlot Slot { get; set; }
    public RosterStatus Status { get; set; }
    public DateTimeOffset EffectiveFromUtc { get; set; }
    public DateTimeOffset? EffectiveToUtc { get; set; }
}
