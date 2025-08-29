using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// Global scoring rule for a given metric (e.g., "bat.run", "bowl.wicket").
/// Leagues can override via LeagueScoringRule.
/// </summary>
public class ScoringRule : AuditableEntity
{
    public Guid Id { get; set; }

    /// <summary>Key for the metric, e.g., "bat.run", "bowl.wicket", "field.catch".</summary>
    public string MetricKey { get; set; } = string.Empty;

    /// <summary>Points per unit; can be negative.</summary>
    public decimal PointsPerUnit { get; set; }

    /// <summary>Whether the metric applies only once (e.g., milestone bonus).</summary>
    public bool ApplyOnce { get; set; }

    /// <summary>Optional conditions JSON (e.g., { "minRuns": 50 }).</summary>
    public string? ConditionsJson { get; set; }
}
