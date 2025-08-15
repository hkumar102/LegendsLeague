namespace LegendsLeague.Contracts.Players;
public sealed class PlayerUpdateRequest
{
    public Guid? RealTeamId { get; set; }
    public string? FullName { get; set; }
    public string? ShortName { get; set; }
    public string? Country { get; set; }
    public PlayerRole? Role { get; set; }
    public BattingStyle? Batting { get; set; }
    public BowlingStyle? Bowling { get; set; }
}
