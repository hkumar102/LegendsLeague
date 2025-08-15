using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// Draft session for a league; can be Snake or Auction.
/// </summary>
public sealed class Draft : AuditableEntity
{
    public Guid Id { get; set; }

    /// <summary>FK → fantasy.leagues.id.</summary>
    public Guid LeagueId { get; set; }

    /// <summary>Draft type (Snake or Auction).</summary>
    public DraftType Type { get; set; } = DraftType.Snake;

    /// <summary>Lifecycle status.</summary>
    public DraftStatus Status { get; set; } = DraftStatus.Scheduled;

    /// <summary>Scheduled start time in UTC.</summary>
    public DateTimeOffset StartsAtUtc { get; set; }

    // Navs
    public FantasyLeague League { get; set; } = default!;
    public ICollection<DraftPick> Picks { get; set; } = new List<DraftPick>();
}
