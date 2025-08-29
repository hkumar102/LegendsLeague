namespace LegendsLeague.Contracts.Fantasy.Scoring;

/// <summary>
/// Aggregated team score for a single fixture (sum of players' fantasy points).
/// </summary>
public sealed class TeamFixtureScoreDto
{
    public Guid LeagueId { get; set; }
    public Guid LeagueTeamId { get; set; }
    public Guid FixtureId { get; set; }
    public decimal TotalPoints { get; set; }
}
