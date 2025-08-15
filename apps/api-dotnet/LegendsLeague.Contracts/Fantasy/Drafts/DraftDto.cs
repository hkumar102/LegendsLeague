namespace LegendsLeague.Contracts.Fantasy;

public sealed record DraftDto(
    Guid Id,
    Guid LeagueId,
    DraftType Type,
    DraftStatus Status,
    DateTimeOffset StartsAtUtc
);
