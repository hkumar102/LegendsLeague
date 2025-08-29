using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fixtures;

/// <summary>
/// Normalized stat line for a player in a specific fixture (provider ingest).
/// Idempotent via (FixtureId, PlayerId, Version) at the Infra layer.
/// </summary>
public class PlayerMatchStats : AuditableEntity
{
    public Guid Id { get; set; }

    public Guid FixtureId { get; set; }
    public Guid PlayerId { get; set; }

    // Batting
    public int Runs { get; set; }
    public int BallsFaced { get; set; }
    public int Fours { get; set; }
    public int Sixes { get; set; }
    public bool NotOut { get; set; }
    public string? DismissalKind { get; set; }

    // Bowling
    public decimal Overs { get; set; }      // 3.4 overs represented as 3.4m
    public int Maidens { get; set; }
    public int RunsConceded { get; set; }
    public int Wickets { get; set; }
    public int NoBalls { get; set; }
    public int Wides { get; set; }

    // Fielding
    public int Catches { get; set; }
    public int RunOuts { get; set; }
    public int Stumpings { get; set; }

    public DateTimeOffset IngestedAtUtc { get; set; }
    public int Version { get; set; } = 1;
}
