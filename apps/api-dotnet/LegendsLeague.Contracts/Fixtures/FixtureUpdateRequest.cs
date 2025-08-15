namespace LegendsLeague.Contracts.Fixtures;

/// <summary>
/// Request body to update an existing fixture.
/// </summary>
public sealed class FixtureUpdateRequest
{
    /// <summary>Optionally change the home team (must remain in same series and not equal AwayTeamId).</summary>
    public Guid? HomeTeamId { get; init; }

    /// <summary>Optionally change the away team (must remain in same series and not equal HomeTeamId).</summary>
    public Guid? AwayTeamId { get; init; }

    /// <summary>Optionally change the start time (UTC).</summary>
    public DateTimeOffset? StartTimeUtc { get; init; }
}
