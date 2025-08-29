using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// A fantasy team owned by a user within a league.
/// </summary>
public sealed class LeagueTeam : SoftDeletableEntity
{
    public Guid Id { get; set; }

    /// <summary>
    /// The league this fantasy team belongs to.
    /// </summary>
    public Guid LeagueId { get; set; }
    public FantasyLeague League { get; set; } = default!;

    /// <summary>
    /// The name of the fantasy team (chosen by the member).
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The member (user) who owns this fantasy team.
    /// </summary>
    public Guid OwnerId { get; set; }
    public LeagueMember Owner { get; set; } = default!;

    /// <summary>
    /// The draft picks made by this team.
    /// </summary>
    public ICollection<DraftPick> Picks { get; set; } = new List<DraftPick>();

    /// <summary>
    /// The roster of players currently on this fantasy team.
    /// </summary>
    public ICollection<RosterPlayer> Roster { get; set; } = new List<RosterPlayer>();
}
