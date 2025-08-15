using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// A single draft selection (round/pick), linking a fantasy team to a real player.
/// </summary>
public sealed class DraftPick : AuditableEntity
{
    public Guid Id { get; set; }

    /// <summary>FK → fantasy.drafts.id.</summary>
    public Guid DraftId { get; set; }

    /// <summary>Round number (1..N) for Snake; Auction may leave null.</summary>
    public int? RoundNo { get; set; }

    /// <summary>Pick number within the round (1..N) for Snake; Auction may leave null.</summary>
    public int? PickNo { get; set; }

    /// <summary>Fantasy team that made the pick (FK → fantasy.league_teams.id).</summary>
    public Guid LeagueTeamId { get; set; }

    /// <summary>Selected real player (FK → fixtures.players.id).</summary>
    public Guid PlayerId { get; set; }

    /// <summary>When the pick was made.</summary>
    public DateTimeOffset MadeAtUtc { get; set; }

    // Navs
    public Draft Draft { get; set; } = default!;
    public LeagueTeam LeagueTeam { get; set; } = default!;
}
