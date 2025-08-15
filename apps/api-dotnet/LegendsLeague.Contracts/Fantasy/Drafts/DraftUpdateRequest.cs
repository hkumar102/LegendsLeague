namespace LegendsLeague.Contracts.Fantasy;

/// <summary>Request payload to update a draft.</summary>
public sealed class DraftUpdateRequest
{
    public DraftType? Type { get; init; }
    public DraftStatus? Status { get; init; }
    public DateTimeOffset? StartsAtUtc { get; init; }
}
