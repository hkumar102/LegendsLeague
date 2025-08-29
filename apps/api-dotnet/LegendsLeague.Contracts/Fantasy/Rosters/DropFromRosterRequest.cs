namespace LegendsLeague.Contracts.Fantasy.Rosters;

/// <summary>Drop a rostered player (close the effective window).</summary>
public sealed class DropFromRosterRequest
{
    public Guid RosterPlayerId { get; set; }
}
