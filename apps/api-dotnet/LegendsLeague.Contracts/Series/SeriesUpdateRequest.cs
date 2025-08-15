namespace LegendsLeague.Contracts.Series;

/// <summary>
/// Request body to update an existing series.
/// </summary>
public sealed class SeriesUpdateRequest
{
    public string Name { get; set; } = string.Empty;
    public int SeasonYear { get; set; }
}
