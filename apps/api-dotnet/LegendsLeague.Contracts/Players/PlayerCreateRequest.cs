namespace LegendsLeague.Contracts.Players;
public sealed class PlayerCreateRequest
{
    public Guid SeriesId { get; set; }
    public Guid RealTeamId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? ShortName { get; set; }
    public string? Country { get; set; }
    public PlayerRole Role { get; set; } = PlayerRole.Batsman;
    public BattingStyle Batting { get; set; } = BattingStyle.Unknown;
    public BowlingStyle Bowling { get; set; } = BowlingStyle.Unknown;
}
