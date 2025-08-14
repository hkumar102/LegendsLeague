using LegendsLeague.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LegendsLeague.Infrastructure.Persistence.Interceptors;

/// <summary>
/// Converts deletes on ISoftDeletable entities into soft deletes:
/// sets IsDeleted=true, DeletedAtUtc, (DeletedBy later), and switches state to Modified.
/// Hard-deleted entities (not implementing ISoftDeletable) are left untouched.
/// </summary>
public sealed class SoftDeleteSaveChangesInterceptor : SaveChangesInterceptor
{
    private static readonly Func<DateTimeOffset> UtcNow = () => DateTimeOffset.UtcNow;

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplySoftDelete(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplySoftDelete(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void ApplySoftDelete(DbContext? context)
    {
        if (context is null) return;

        var now = UtcNow();

        foreach (var entry in context.ChangeTracker.Entries<ISoftDeletable>())
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted    = true;
                entry.Entity.DeletedAtUtc = now;
                // entry.Entity.DeletedBy = currentUserId; // wire later via ICurrentUser
            }
        }
    }
}
