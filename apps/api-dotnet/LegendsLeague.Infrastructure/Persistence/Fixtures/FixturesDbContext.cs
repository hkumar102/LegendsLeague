using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Domain.Entities.Fixtures;
using LegendsLeague.Infrastructure.Persistence.Extensions;
using LegendsLeague.Infrastructure.Persistence.ModelBuilding;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL; // for HasPostgresExtension

namespace LegendsLeague.Infrastructure.Persistence.Fixtures
{
    /// <summary>
    /// EF Core DbContext for the Fixtures bounded context.
    /// Uses the <c>fixtures</c> schema and enforces snake_case naming for all database objects.
    /// Implements <see cref="IFixturesDbContext"/> so the Application layer can depend on an abstraction.
    /// </summary>
    public class FixturesDbContext : DbContext, IFixturesDbContext
    {
        public FixturesDbContext(DbContextOptions<FixturesDbContext> options) : base(options) { }

        public DbSet<Series> Series => Set<Series>();
        public DbSet<RealTeam> RealTeams => Set<RealTeam>();
        public DbSet<Fixture> Fixtures => Set<Fixture>();

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("fixtures");

            // Enable case-insensitive text type (citext) for Postgres
            modelBuilder.HasPostgresExtension("citext");

            // Load mappings and conventions
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FixturesDbContext).Assembly);
            modelBuilder.UseSnakeCaseNames();
            modelBuilder.ApplySoftDeleteQueryFilters();

            base.OnModelCreating(modelBuilder);
        }
    }
}
