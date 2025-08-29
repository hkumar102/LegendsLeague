namespace LegendsLeague.Contracts.Fantasy.Scoring;

/// <summary>
/// League standings row (aggregated over fixtures).
/// </summary>
public sealed class LeagueStandingDto
{
    public Guid LeagueId { get; set; }
    public Guid LeagueTeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public decimal Points { get; set; }       // overall points (or wins for H2H)
    public int Rank { get; set; }
}
