using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// League-scoped override for a scoring rule (per MetricKey).
/// Null property means inherit from global definition.
/// </summary>
public class LeagueScoringRule : AuditableEntity
{
    public Guid Id { get; set; }

    public Guid LeagueId { get; set; }
    public FantasyLeague League { get; set; } = default!;

    public string MetricKey { get; set; } = string.Empty;

    public decimal? PointsPerUnit { get; set; }
    public bool? ApplyOnce { get; set; }
    public string? ConditionsJson { get; set; }
}
