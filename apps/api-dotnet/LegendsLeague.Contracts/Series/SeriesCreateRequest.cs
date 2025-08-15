namespace LegendsLeague.Contracts.Series;

/// <summary>
/// Request body to create a new series.
/// </summary>
public sealed class SeriesCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public int SeasonYear { get; set; }
}
