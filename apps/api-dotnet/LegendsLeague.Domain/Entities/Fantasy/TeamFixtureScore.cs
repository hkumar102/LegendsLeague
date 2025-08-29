using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// Aggregated fantasy score for a team in a single fixture (sum of player scores).
/// </summary>
public class TeamFixtureScore : AuditableEntity
{
    public Guid Id { get; set; }

    public Guid LeagueId { get; set; }
    public FantasyLeague League { get; set; } = default!;

    public Guid LeagueTeamId { get; set; }
    public LeagueTeam LeagueTeam { get; set; } = default!;

    public Guid FixtureId { get; set; } // Fixtures.Fixture (scalar)

    public decimal TotalPoints { get; set; }
}
