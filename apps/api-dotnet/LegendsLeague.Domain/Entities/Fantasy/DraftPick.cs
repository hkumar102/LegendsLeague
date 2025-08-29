using LegendsLeague.Domain.Common;
using LegendsLeague.Domain.Entities.Fixtures;

namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// Represents a single pick made during a fantasy draft.
/// Each pick assigns a player to a league team at a specific slot.
/// </summary>
public class DraftPick : AuditableEntity
{
    public Guid Id { get; set; }

    /// <summary>
    /// The draft this pick belongs to.
    /// </summary>
    public Guid DraftId { get; set; }
    public Draft Draft { get; set; } = default!;

    /// <summary>
    /// The league team making the pick.
    /// </summary>
    public Guid LeagueTeamId { get; set; }
    public LeagueTeam LeagueTeam { get; set; } = default!;

    /// <summary>
    /// The player chosen in this pick.
    /// </summary>
    public Guid PlayerId { get; set; }
    public Player Player { get; set; } = default!;

    /// <summary>
    /// Overall pick number in the draft (1, 2, 3...).
    /// </summary>
    public int OverallPickNumber { get; set; }

    /// <summary>
    /// The round number (useful in snake drafts).
    /// </summary>
    public int RoundNumber { get; set; }

    /// <summary>
    /// Position of the pick within the round.
    /// </summary>
    public int PickInRound { get; set; }

    /// <summary>
    /// Current status of the pick (Pending, Completed, Skipped).
    /// </summary>
    public DraftPickStatus Status { get; set; } = DraftPickStatus.Pending;

    /// <summary>
    /// Timestamp when the pick was locked in.
    /// </summary>
    public DateTimeOffset? PickedAtUtc { get; set; }
}