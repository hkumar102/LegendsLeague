namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>
/// Centralized Fantasy enums for domain. 
/// NOTE: Values mirror Contracts exactly. Do not change existing numeric values.
/// </summary>
public static class _FantasyEnumsAnchor { }

/// <summary>Role of a member within a fantasy league.</summary>
public enum LeagueMemberRole
{
    Commissioner   = 1,
    Member         = 2,
    CoCommissioner = 3
}

/// <summary>Draft types supported by the platform.</summary>
public enum DraftType
{
    Snake   = 1,
    Auction = 2
}

/// <summary>Status of a draft lifecycle.</summary>
public enum DraftStatus
{
    Scheduled = 1,
    Live      = 2,
    Completed = 3,
    Cancelled = 4
}

/// <summary>Status of an individual draft pick.</summary>
public enum DraftPickStatus
{
    Pending   = 1,
    Completed = 2,
    Skipped   = 3,
    AutoPick  = 4
}

/// <summary>Optional scoring models (future-proof).</summary>
public enum ScoringType
{
    Points     = 1,
    HeadToHead = 2,
    Categories = 3
}

/// <summary>Roster slot types (role-based positions).</summary>
public enum RosterSlot
{
    BAT   = 1,
    BWL   = 2,
    AR    = 3,
    WK    = 4,
    BENCH = 9
}

/// <summary>Roster status states for players over time.</summary>
public enum RosterStatus
{
    Active  = 1,
    Bench   = 2,
    Injured = 3,
    Dropped = 4
}

public enum LeagueMemberStatus
{
    Invited   = 0,
    Active    = 1,
    Declined  = 2,
    Removed   = 3
}
