namespace LegendsLeague.Contracts.Fantasy;

/// <summary>Request payload to create a draft.</summary>
public sealed class DraftCreateRequest
{
    public Guid LeagueId { get; init; }
    public DraftType Type { get; init; } = DraftType.Snake;
    public DraftStatus Status { get; init; } = DraftStatus.Scheduled;
    public DateTimeOffset StartsAtUtc { get; init; }
}
