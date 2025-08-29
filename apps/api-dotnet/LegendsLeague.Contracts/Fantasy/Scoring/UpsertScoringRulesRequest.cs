namespace LegendsLeague.Contracts.Fantasy.Scoring;

/// <summary>
/// Bulk upsert/override scoring rules for a league.
/// </summary>
public sealed class UpsertScoringRulesRequest
{
    public Guid LeagueId { get; set; }
    public IReadOnlyList<LeagueScoringRuleDto> Rules { get; set; } = Array.Empty<LeagueScoringRuleDto>();
}
