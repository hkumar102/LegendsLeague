namespace LegendsLeague.Contracts.Fantasy;

/// <summary>Request to create a draft for a league.</summary>
public sealed class CreateDraftRequest
{
    public DraftType DraftType { get; set; }
    public DateTimeOffset ScheduledAtUtc { get; set; }
}
