using LegendsLeague.Domain.Abstractions.Security;
using LegendsLeague.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LegendsLeague.Infrastructure.Persistence.Interceptors;

/// <summary>
/// Converts Delete operations on <see cref="ISoftDeletable"/> entities into soft deletes:
/// sets IsDeleted=true, DeletedAtUtc, and DeletedBy, and flips state to Modified.
/// </summary>
public sealed class SoftDeleteSaveChangesInterceptor : SaveChangesInterceptor
{
    private static readonly Func<DateTimeOffset> NowUtc = () => DateTimeOffset.UtcNow;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="SoftDeleteSaveChangesInterceptor"/> class.
    /// </summary>
    /// <param name="currentUser">Provider for current user identity.</param>
    public SoftDeleteSaveChangesInterceptor(ICurrentUser currentUser) => _currentUser = currentUser;

    /// <inheritdoc />
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplySoftDelete(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    /// <inheritdoc />
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplySoftDelete(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplySoftDelete(DbContext? context)
    {
        if (context is null) return;

        var now = NowUtc();
        var userId = _currentUser.UserId;

        foreach (var entry in context.ChangeTracker.Entries<ISoftDeletable>())
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted    = true;
                entry.Entity.DeletedAtUtc = now;
                if (userId.HasValue) entry.Entity.DeletedBy = userId;
            }
        }
    }
}
