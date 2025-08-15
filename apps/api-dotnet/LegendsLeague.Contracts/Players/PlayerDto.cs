namespace LegendsLeague.Contracts.Players;
public sealed record PlayerDto(
    Guid Id,
    Guid SeriesId,
    Guid RealTeamId,
    string FullName,
    string? ShortName,
    string? Country,
    PlayerRole Role,
    BattingStyle Batting,
    BowlingStyle Bowling
);
