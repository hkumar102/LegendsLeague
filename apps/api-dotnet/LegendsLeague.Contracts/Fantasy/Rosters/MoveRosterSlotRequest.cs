using LegendsLeague.Contracts.Fantasy;

namespace LegendsLeague.Contracts.Fantasy.Rosters;

/// <summary>Move a rostered player to a new slot (e.g., BENCH → BAT).</summary>
public sealed class MoveRosterSlotRequest
{
    public Guid RosterPlayerId { get; set; }
    public RosterSlot NewSlot { get; set; }
}
