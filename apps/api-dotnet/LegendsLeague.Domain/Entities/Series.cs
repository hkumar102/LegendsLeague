namespace LegendsLeague.Domain.Entities;

public class Series
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SeasonYear { get; set; }
}
