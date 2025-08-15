using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// A user-created fantasy league scoped to a single real-world series (fixtures.series).
/// </summary>
public sealed class FantasyLeague : AuditableEntity
{
    /// <summary>Primary key.</summary>
    public Guid Id { get; set; }

    /// <summary>Series this league is tied to (FK → fixtures.series.id).</summary>
    public Guid SeriesId { get; set; }

    /// <summary>Human-friendly name of the league.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Maximum number of fantasy teams allowed in the league.</summary>
    public int MaxTeams { get; set; }

    /// <summary>User id of the commissioner/creator (from your auth system).</summary>
    public Guid CommissionerUserId { get; set; }

    // Navs
    public ICollection<LeagueMember> Members { get; set; } = new List<LeagueMember>();
    public ICollection<LeagueTeam> Teams { get; set; } = new List<LeagueTeam>();
    public ICollection<Draft> Drafts { get; set; } = new List<Draft>();
}
