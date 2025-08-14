namespace LegendsLeague.Domain.Common;

public interface IAuditable
{
    DateTimeOffset CreatedAtUtc { get; set; }
    Guid?          CreatedBy    { get; set; }

    DateTimeOffset? ModifiedAtUtc { get; set; }
    Guid?           ModifiedBy    { get; set; }
}
