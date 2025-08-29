namespace LegendsLeague.Contracts.Fantasy.Scoring;

/// <summary>
/// Computed fantasy score for a single player in a fixture for a given league.
/// </summary>
public sealed class FantasyScoreDto
{
    public Guid LeagueId { get; set; }
    public Guid LeagueTeamId { get; set; }
    public Guid FixtureId { get; set; }
    public Guid PlayerId { get; set; }

    public decimal TotalPoints { get; set; }
    public DateTimeOffset CalculatedAtUtc { get; set; }
    public int Version { get; set; } = 1;

    public string? BreakdownJson { get; set; }   // metric → points mapping for UI
}
