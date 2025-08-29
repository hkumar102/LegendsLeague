using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// Computed fantasy score for a player in a fixture for a given league/team.
/// </summary>
public class FantasyScore : AuditableEntity
{
    public Guid Id { get; set; }

    public Guid LeagueId { get; set; }
    public FantasyLeague League { get; set; } = default!;

    public Guid LeagueTeamId { get; set; }
    public LeagueTeam LeagueTeam { get; set; } = default!;

    public Guid FixtureId { get; set; } // Fixtures.Fixture (scalar)
    public Guid PlayerId { get; set; }  // Fixtures.Player  (scalar)

    public decimal TotalPoints { get; set; }

    /// <summary>Metric→points breakdown serialized for UI/debug.</summary>
    public string? BreakdownJson { get; set; }

    /// <summary>Version increases if provider corrections occur.</summary>
    public int Version { get; set; } = 1;

    public DateTimeOffset CalculatedAtUtc { get; set; }
}
