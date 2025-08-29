namespace LegendsLeague.Contracts.Fantasy.Rosters;

/// <summary>Bench a rostered player (without dropping them).</summary>
public sealed class BenchPlayerRequest
{
    public Guid RosterPlayerId { get; set; }
}
