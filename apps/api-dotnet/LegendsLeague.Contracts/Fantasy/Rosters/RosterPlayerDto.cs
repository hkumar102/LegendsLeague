using LegendsLeague.Contracts.Players;

namespace LegendsLeague.Contracts.Fantasy;

public sealed record RosterPlayerDto(
    Guid Id,
    Guid RosterId,
    Guid PlayerId,
    string PlayerName,
    PlayerRole Role,
    DateTimeOffset AddedAtUtc
);
