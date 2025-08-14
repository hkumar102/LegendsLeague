namespace LegendsLeague.Domain.Common;

/// <summary>
/// Base class for entities that require Created/Modified auditing.
/// Use <see cref="SoftDeletableEntity"/> if you also want soft-delete fields.
/// </summary>
public abstract class AuditableEntity : IAuditable
{
    public DateTimeOffset CreatedAtUtc  { get; set; }
    public Guid?          CreatedBy     { get; set; }

    public DateTimeOffset? ModifiedAtUtc { get; set; }
    public Guid?           ModifiedBy    { get; set; }
}
