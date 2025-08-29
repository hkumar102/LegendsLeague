using LegendsLeague.Contracts.Fantasy;

namespace LegendsLeague.Contracts.Fantasy.Rosters;

/// <summary>Request to add a player to a team's roster.</summary>
public sealed class AddToRosterRequest
{
    public Guid PlayerId { get; set; }
    public RosterSlot Slot { get; set; }
}
