namespace LegendsLeague.Contracts.Fixtures;

/// <summary>
/// Request body to create a new fixture within a given series.
/// Note: In our API, <c>SeriesId</c> typically comes from the route (e.g. /series/{seriesId}/fixtures).
/// </summary>
public sealed class FixtureCreateRequest
{
    /// <summary>Home team (real team) id. Must belong to the same series.</summary>
    public Guid HomeTeamId { get; init; }

    /// <summary>Away team (real team) id. Must belong to the same series and not equal HomeTeamId.</summary>
    public Guid AwayTeamId { get; init; }

    /// <summary>First ball / start time in UTC.</summary>
    public DateTimeOffset StartTimeUtc { get; init; }
}
