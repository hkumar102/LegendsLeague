using LegendsLeague.Infrastructure.Persistence.Fixtures;
using LegendsLeague.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Infrastructure.Persistence.Fantasy;

namespace LegendsLeague.Infrastructure.Persistence;

/// <summary>
/// Registers persistence services and DbContexts for the application.
/// Uses a single Postgres connection string for all module DbContexts.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds all persistence services (DbContexts + EF Core interceptors).
    /// </summary>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var cs = config.GetConnectionString("DefaultConnection")
                 ?? "Host=localhost;Port=5432;Database=legends_league;Username=legends_app;Password=change_me_strong_pw";

        // Interceptors as scoped services (depend on ICurrentUser)
        services.AddScoped<AuditingSaveChangesInterceptor>();
        services.AddScoped<SoftDeleteSaveChangesInterceptor>();

        // Fixtures DbContext (existing)
        services.AddDbContext<Fixtures.FixturesDbContext>((sp, opt) =>
        {
            opt.UseNpgsql(cs, npg =>
            {
                npg.MigrationsHistoryTable("__efmigrations_history", "fixtures");
            });
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            opt.AddInterceptors(
                sp.GetRequiredService<AuditingSaveChangesInterceptor>(),
                sp.GetRequiredService<SoftDeleteSaveChangesInterceptor>());
        });
        services.AddScoped<IFixturesDbContext>(sp =>
            sp.GetRequiredService<FixturesDbContext>());

        // Fantasy DbContext (NEW)
        services.AddDbContext<FantasyDbContext>((sp, opt) =>
        {
            opt.UseNpgsql(cs, npg =>
            {
                npg.MigrationsHistoryTable("__efmigrations_history", "fantasy");
            });
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            opt.AddInterceptors(
                sp.GetRequiredService<AuditingSaveChangesInterceptor>(),
                sp.GetRequiredService<SoftDeleteSaveChangesInterceptor>());
        });

        return services;
    }
}
