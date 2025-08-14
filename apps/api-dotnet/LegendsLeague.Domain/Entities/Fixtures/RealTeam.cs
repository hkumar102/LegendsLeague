using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fixtures;

public class RealTeam : SoftDeletableEntity
{
    public Guid Id { get; set; }
    public Guid SeriesId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ShortName { get; set; }

    public Series Series { get; set; } = default!;
}
