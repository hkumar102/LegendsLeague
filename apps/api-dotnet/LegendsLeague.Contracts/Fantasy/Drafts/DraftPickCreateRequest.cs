namespace LegendsLeague.Contracts.Fantasy;

/// <summary>Request payload to record a draft pick.</summary>
public sealed class DraftPickCreateRequest
{
    public Guid DraftId { get; init; }
    public int? RoundNo { get; init; }
    public int? PickNo { get; init; }
    public Guid LeagueTeamId { get; init; }
    public Guid PlayerId { get; init; }
    public DateTimeOffset MadeAtUtc { get; init; }
}
