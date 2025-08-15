namespace LegendsLeague.Contracts.Teams;

/// <summary>
/// Data transfer object representing a real team within a specific series.
/// </summary>
/// <param name="Id">Team identifier.</param>
/// <param name="SeriesId">Owning series identifier.</param>
/// <param name="Name">Full team name (e.g., "Mumbai Indians").</param>
/// <param name="ShortName">Short code (e.g., "MI").</param>
public sealed record RealTeamDto(
    Guid Id,
    Guid SeriesId,
    string Name,
    string? ShortName
);
