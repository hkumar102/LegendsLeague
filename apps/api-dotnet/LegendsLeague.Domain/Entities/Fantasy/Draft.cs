using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// Represents a fantasy draft belonging to a league.
/// Each league has exactly one draft defining how players are assigned.
/// </summary>
public class Draft : AuditableEntity
{
    public Guid Id { get; set; }

    /// <summary>
    /// Owning league.
    /// </summary>
    public Guid LeagueId { get; set; }
    public FantasyLeague League { get; set; } = default!;

    /// <summary>
    /// Draft type (e.g., Snake, Auction).
    /// </summary>
    public DraftType DraftType { get; set; }

    /// <summary>
    /// Current draft status (Scheduled, Live, Completed).
    /// </summary>
    public DraftStatus Status { get; set; } = DraftStatus.Scheduled;

    /// <summary>
    /// When the draft is scheduled to begin.
    /// </summary>
    public DateTimeOffset? ScheduledAtUtc { get; set; }

    /// <summary>
    /// When the draft actually started.
    /// </summary>
    public DateTimeOffset? StartedAtUtc { get; set; }

    /// <summary>
    /// When the draft completed.
    /// </summary>
    public DateTimeOffset? CompletedAtUtc { get; set; }

    /// <summary>
    /// Picks made in this draft.
    /// </summary>
    public ICollection<DraftPick> Picks { get; set; } = new List<DraftPick>();
}