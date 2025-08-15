namespace LegendsLeague.Contracts.Teams;

/// <summary>
/// Request payload to update an existing team.
/// </summary>
public sealed class TeamUpdateRequest
{
    /// <summary>Full team name (unique within series, case-insensitive).</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Optional short code (<= 10 chars).</summary>
    public string? ShortName { get; set; }
}
