using LegendsLeague.Domain.Abstractions.Security;
using LegendsLeague.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LegendsLeague.Infrastructure.Persistence.Interceptors;

/// <summary>
/// Intercepts SaveChanges to populate Created*/Modified* fields on <see cref="IAuditable"/> entities.
/// </summary>
public sealed class AuditingSaveChangesInterceptor : SaveChangesInterceptor
{
    private static readonly Func<DateTimeOffset> NowUtc = () => DateTimeOffset.UtcNow;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditingSaveChangesInterceptor"/> class.
    /// </summary>
    /// <param name="currentUser">Provider for current user identity.</param>
    public AuditingSaveChangesInterceptor(ICurrentUser currentUser) => _currentUser = currentUser;

    /// <inheritdoc />
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    /// <inheritdoc />
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyAudit(DbContext? context)
    {
        if (context is null) return;

        var now = NowUtc();
        var userId = _currentUser.UserId;

        foreach (var entry in context.ChangeTracker.Entries<IAuditable>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAtUtc = now;
                if (userId.HasValue) entry.Entity.CreatedBy = userId;
            }
            else if (entry.State == EntityState.Modified)
            {
                // preserve original Created*
                entry.Property(nameof(IAuditable.CreatedAtUtc)).IsModified = false;
                entry.Property(nameof(IAuditable.CreatedBy)).IsModified    = false;

                entry.Entity.ModifiedAtUtc = now;
                if (userId.HasValue) entry.Entity.ModifiedBy = userId;
            }
        }
    }
}
