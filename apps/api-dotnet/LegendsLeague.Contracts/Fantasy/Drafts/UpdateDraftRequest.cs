namespace LegendsLeague.Contracts.Fantasy;

/// <summary>Request to update a scheduled draft.</summary>
public sealed class UpdateDraftRequest
{
    public DraftType DraftType { get; set; }
    public DateTimeOffset ScheduledAtUtc { get; set; }
}
