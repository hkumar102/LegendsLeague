namespace LegendsLeague.Domain.Entities.Fantasy;

/// <summary>Member role within a fantasy league.</summary>
public enum LeagueMemberRole
{
    Commissioner = 1,
    Member = 2
}

/// <summary>Draft mechanism style.</summary>
public enum DraftType
{
    Snake = 1,
    Auction = 2
}

/// <summary>Draft lifecycle status.</summary>
public enum DraftStatus
{
    Scheduled = 1,
    Live = 2,
    Completed = 3,
    Cancelled = 4
}

/// <summary>Roster slot category for a fantasy team.</summary>
public enum RosterSlot
{
    BAT = 1,    // Batter
    BWL = 2,    // Bowler
    AR  = 3,    // All-Rounder
    WK  = 4,    // Wicket-Keeper
    BENCH = 9
}
