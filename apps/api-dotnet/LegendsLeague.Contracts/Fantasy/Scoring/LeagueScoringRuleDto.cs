namespace LegendsLeague.Contracts.Fantasy.Scoring;

/// <summary>
/// Scoring rule override scoped to a league.
/// </summary>
public sealed class LeagueScoringRuleDto
{
    public Guid LeagueId { get; set; }
    public string MetricKey { get; set; } = string.Empty;
    public decimal? PointsPerUnit { get; set; }   // null => inherit global
    public bool? ApplyOnce { get; set; }          // null => inherit global
    public string? ConditionsJson { get; set; }   // null => inherit global
}
