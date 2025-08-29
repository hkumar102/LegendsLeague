namespace LegendsLeague.Contracts.Fantasy.Scoring;

/// <summary>
/// Normalized, per-fixture stat line for a player (input to the fantasy scorer).
/// </summary>
public sealed class PlayerMatchStatsDto
{
    public Guid FixtureId { get; set; }  // from Fixtures context (scalar)
    public Guid PlayerId  { get; set; }  // from Fixtures context (scalar)

    // Batting
    public int Runs { get; set; }
    public int BallsFaced { get; set; }
    public int Fours { get; set; }
    public int Sixes { get; set; }
    public bool NotOut { get; set; }
    public string? DismissalKind { get; set; } // e.g., "lbw", "caught"

    // Bowling
    public decimal Overs { get; set; }          // e.g., 3.4 overs => 3.4m (use decimal to carry balls)
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
    public int Version { get; set; } = 1;  // bump when provider issues corrections
}
