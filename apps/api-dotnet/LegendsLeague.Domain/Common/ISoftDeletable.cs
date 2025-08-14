namespace LegendsLeague.Domain.Common;

public interface ISoftDeletable
{
    bool            IsDeleted    { get; set; }
    DateTimeOffset? DeletedAtUtc { get; set; }
    Guid?           DeletedBy    { get; set; }
}
