using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// A fantasy team owned by a user within a league.
/// </summary>
public sealed class LeagueTeam : SoftDeletableEntity
{
    public Guid Id { get; set; }

    /// <summary>FK → fantasy.leagues.id.</summary>
    public Guid LeagueId { get; set; }

    /// <summary>Friendly team name chosen by the owner.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Owner's external user id.</summary>
    public Guid OwnerUserId { get; set; }

    /// <summary>Initial draft position (1..N) if Snake draft; optional otherwise.</summary>
    public int? DraftPosition { get; set; }

    // Navs
    public FantasyLeague League { get; set; } = default!;
    public ICollection<RosterPlayer> Roster { get; set; } = new List<RosterPlayer>();
}
