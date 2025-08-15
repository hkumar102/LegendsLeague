namespace LegendsLeague.Contracts.Fixtures;

/// <summary>
/// Represents a scheduled cricket match (fixture) within a series.
/// </summary>
/// <param name="Id">Fixture identifier.</param>
/// <param name="SeriesId">Series in which the fixture takes place.</param>
/// <param name="HomeTeamId">Home team identifier (real team).</param>
/// <param name="AwayTeamId">Away team identifier (real team).</param>
/// <param name="StartTimeUtc">Kick-off / first ball time in UTC.</param>
public sealed record FixtureDto(
    Guid Id,
    Guid SeriesId,
    Guid HomeTeamId,
    Guid AwayTeamId,
    DateTimeOffset StartTimeUtc
);
