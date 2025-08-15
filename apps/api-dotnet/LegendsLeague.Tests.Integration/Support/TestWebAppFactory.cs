using System.Linq;
using LegendsLeague.Domain.Entities.Fixtures;
using LegendsLeague.Infrastructure.Persistence.Fixtures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LegendsLeague.Tests.Integration.Support;

/// <summary>
/// Boots the API in-memory for tests and replaces the FixturesDbContext with EF InMemory.
/// Also removes Npgsql provider services to avoid multiple provider registration.
/// </summary>
public sealed class TestWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // 1) Remove existing DbContext registrations for FixturesDbContext (Postgres)
            var toRemove = services
                .Where(d =>
                    d.ServiceType == typeof(DbContextOptions<FixturesDbContext>) ||
                    d.ServiceType == typeof(FixturesDbContext))
                .ToList();
            foreach (var d in toRemove) services.Remove(d);

            // 2) Remove Npgsql provider-level registrations to prevent "multiple providers" error
            //    We strip any service whose ImplementationType OR ServiceType comes from the Npgsql EFCore assembly.
            var npgsqlTagged = services
                .Where(d =>
                    (d.ImplementationType?.Assembly.FullName?.Contains("Npgsql.EntityFrameworkCore.PostgreSQL") ?? false) ||
                    (d.ServiceType?.Assembly.FullName?.Contains("Npgsql.EntityFrameworkCore.PostgreSQL") ?? false))
                .ToList();
            foreach (var d in npgsqlTagged) services.Remove(d);

            // 3) Register InMemory provider for FixturesDbContext
            services.AddDbContext<FixturesDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("fixtures-testdb");
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            // 4) Build and seed
            using var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FixturesDbContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            // Seed Series
            var seed = new[]
            {
                new Series { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), Name = "Indian Premier League", SeasonYear = 2025 },
                new Series { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), Name = "Indian Premier League", SeasonYear = 2026 },
                new Series { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"), Name = "ICC T20 World Cup",    SeasonYear = 2026 },
                new Series { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"), Name = "Big Bash League",       SeasonYear = 2024 }
            };

            db.Series.AddRange(seed);
            db.SaveChanges();
        });
    }
}
