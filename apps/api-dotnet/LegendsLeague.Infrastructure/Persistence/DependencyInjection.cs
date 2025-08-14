using LegendsLeague.Infrastructure.Persistence.Fixtures;
using LegendsLeague.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LegendsLeague.Infrastructure.Persistence;

public static class DependencyInjection
{
    /// <summary>
    /// Registers Infrastructure persistence (DbContexts) using ONE Postgres connection string.
    /// Also adds SaveChanges interceptors for auditing and soft-delete.
    /// </summary>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var cs = config.GetConnectionString("postgres")
                 ?? "Host=localhost;Port=5432;Database=legends_league;Username=postgres;Password=postgres";

        // Interceptors as scoped services (so they can depend on ICurrentUser later)
        services.AddScoped<AuditingSaveChangesInterceptor>();
        services.AddScoped<SoftDeleteSaveChangesInterceptor>();

        // Fixtures module DbContext (schema: fixtures)
        services.AddDbContext<FixturesDbContext>((sp, opt) =>
        {
            opt.UseNpgsql(cs, npg =>
            {
                npg.MigrationsHistoryTable("__efmigrations_history", "fixtures");
            });
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            // Attach interceptors
            opt.AddInterceptors(
                sp.GetRequiredService<AuditingSaveChangesInterceptor>(),
                sp.GetRequiredService<SoftDeleteSaveChangesInterceptor>());
        });

        return services;
    }
}
