namespace LegendsLeague.Contracts.Fantasy;

public sealed record DraftPickDto(
    Guid Id,
    Guid DraftId,
    int? RoundNo,
    int? PickNo,
    Guid LeagueTeamId,
    Guid PlayerId,
    DateTimeOffset MadeAtUtc
);
