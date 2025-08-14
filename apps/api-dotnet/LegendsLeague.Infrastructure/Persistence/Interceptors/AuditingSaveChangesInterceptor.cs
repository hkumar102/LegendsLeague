using LegendsLeague.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LegendsLeague.Infrastructure.Persistence.Interceptors;

/// <summary>
/// Sets Created*/Modified* fields for entities implementing IAuditable.
/// Uses DateTimeOffset.UtcNow; CreatedBy/ModifiedBy can be set later via ICurrentUser.
/// </summary>
public sealed class AuditingSaveChangesInterceptor : SaveChangesInterceptor
{
    private static readonly Func<DateTimeOffset> UtcNow = () => DateTimeOffset.UtcNow;

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void ApplyAudit(DbContext? context)
    {
        if (context is null) return;

        var now = UtcNow();

        foreach (var entry in context.ChangeTracker.Entries<IAuditable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAtUtc = now;
                    // entry.Entity.CreatedBy = currentUserId; // wire later via ICurrentUser
                    break;
                case EntityState.Modified:
                    entry.Property(nameof(IAuditable.CreatedAtUtc)).IsModified = false;
                    entry.Property(nameof(IAuditable.CreatedBy)).IsModified    = false;

                    entry.Entity.ModifiedAtUtc = now;
                    // entry.Entity.ModifiedBy = currentUserId; // wire later via ICurrentUser
                    break;
            }
        }
    }
}
