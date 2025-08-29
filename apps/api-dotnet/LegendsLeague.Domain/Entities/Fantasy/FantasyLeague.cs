using LegendsLeague.Domain.Common;
using LegendsLeague.Domain.Entities.Fixtures;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// A user-created fantasy league scoped to a single real-world series (fixtures.series).
/// </summary>
public sealed class FantasyLeague : AuditableEntity
{
    public Guid Id { get; set; }

    /// <summary>
    /// Human-friendly league name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The series (real-world competition) this fantasy league is tied to.
    /// Example: IPL 2026.
    /// </summary>
    public Guid SeriesId { get; set; }
    public Series Series { get; set; } = default!;

    /// <summary>
    /// User ID of the league commissioner (creator).
    /// </summary>
    public Guid CommissionerId { get; set; }

    /// <summary>
    /// The number of teams allowed in the league.
    /// </summary>
    public int MaxTeams { get; set; }

    /// <summary>
    /// Collection of teams participating in this league.
    /// </summary>
    public ICollection<LeagueTeam> Teams { get; set; } = new List<LeagueTeam>();

    /// <summary>
    /// Collection of members (users) in the league.
    /// </summary>
    public ICollection<LeagueMember> Members { get; set; } = new List<LeagueMember>();

    /// <summary>
    /// The draft associated with this league.
    /// </summary>
    public Draft Draft { get; set; } = default!;
}
