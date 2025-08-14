using System.Linq.Expressions;
using LegendsLeague.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LegendsLeague.Infrastructure.Persistence.ModelBuilding;

public static class ModelBuilderSoftDeleteExtensions
{
    /// <summary>
    /// Adds a global query filter for entities implementing ISoftDeletable (IsDeleted == false).
    /// Call this from OnModelCreating(). Safe to call multiple times.
    /// </summary>
    public static void ApplySoftDeleteQueryFilters(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var prop = Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
                var compare = Expression.Equal(prop, Expression.Constant(false));
                var lambda = Expression.Lambda(compare, parameter);

                entityType.SetQueryFilter(lambda);
            }
        }
    }
}
