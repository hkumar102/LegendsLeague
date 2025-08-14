namespace LegendsLeague.Contracts.Series;

/// <summary>
/// Data transfer object representing a cricket series/season (e.g., IPL 2026).
/// This is the shape returned by the API and consumed by clients.
/// </summary>
/// <param name="Id">Series identifier.</param>
/// <param name="Name">Human-friendly series name (e.g., "Indian Premier League").</param>
/// <param name="SeasonYear">Year of the season (e.g., 2026).</param>
public sealed record SeriesDto(
    Guid Id,
    string Name,
    int SeasonYear
);
