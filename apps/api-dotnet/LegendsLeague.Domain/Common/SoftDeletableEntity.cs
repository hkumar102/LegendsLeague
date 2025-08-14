namespace LegendsLeague.Domain.Common;

/// <summary>
/// Base class for entities that are auditable AND soft-deletable.
/// If an entity should be hard-deleted, inherit from <see cref="AuditableEntity"/> instead.
/// </summary>
public abstract class SoftDeletableEntity : AuditableEntity, ISoftDeletable
{
    public bool            IsDeleted    { get; set; }
    public DateTimeOffset? DeletedAtUtc { get; set; }
    public Guid?           DeletedBy    { get; set; }
}
