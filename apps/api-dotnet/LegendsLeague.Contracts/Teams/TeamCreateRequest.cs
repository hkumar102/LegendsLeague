namespace LegendsLeague.Contracts.Teams;

/// <summary>
/// Request payload to create a new team in a series.
/// </summary>
public sealed class TeamCreateRequest
{
    /// <summary>Full team name (unique within series, case-insensitive).</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Optional short code (<= 10 chars).</summary>
    public string? ShortName { get; set; }
}
