namespace LegendsLeague.Contracts.Fantasy;

/// <summary>Request to make a pick in a live draft.</summary>
public sealed class MakePickRequest
{
    public Guid LeagueTeamId { get; set; }
    public Guid PlayerId { get; set; }
    public int OverallPickNumber { get; set; }
}
