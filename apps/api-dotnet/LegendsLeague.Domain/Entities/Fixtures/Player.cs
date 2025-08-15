using LegendsLeague.Domain.Common;
using LegendsLeague.Domain.Entities.Fixtures.Enums;

namespace LegendsLeague.Domain.Entities.Fixtures;

/// <summary>
/// Real-world cricket player participating in a specific series and assigned to a real team.
/// </summary>
public class Player : SoftDeletableEntity
{
    public Guid Id { get; set; }
    public Guid SeriesId { get; set; }
    public Guid RealTeamId { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string? ShortName { get; set; }
    public string? Country { get; set; }

    public PlayerRole Role { get; set; } = PlayerRole.Batsman;
    public BattingStyle Batting { get; set; } = BattingStyle.Unknown;
    public BowlingStyle Bowling { get; set; } = BowlingStyle.Unknown;

    public Series Series { get; set; } = default!;
    public RealTeam RealTeam { get; set; } = default!;
}
