namespace LegendsLeague.Contracts.Fantasy.Scoring;

/// <summary>
/// A single scoring rule (global default). Leagues can override points per metric.
/// </summary>
public sealed class ScoringRuleDto
{
    public string MetricKey { get; set; } = string.Empty;   // e.g., "bat.run", "bowl.wicket", "field.catch", "bat.fifty_bonus"
    public decimal PointsPerUnit { get; set; }              // can be negative (e.g., dot-ball penalties)
    public bool ApplyOnce { get; set; }                     // true for milestone bonuses
    public string? ConditionsJson { get; set; }             // e.g., { "minRuns": 50 }
}
