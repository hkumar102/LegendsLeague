using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fixtures;

public class Fixture : SoftDeletableEntity
{
    public Guid Id { get; set; }
    public Guid SeriesId { get; set; }
    public Guid HomeTeamId { get; set; }
    public Guid AwayTeamId { get; set; }

    public DateTimeOffset StartTimeUtc { get; set; }

    public Series Series { get; set; } = default!;
    public RealTeam HomeTeam { get; set; } = default!;
    public RealTeam AwayTeam { get; set; } = default!;
}
