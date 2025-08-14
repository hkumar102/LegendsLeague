using LegendsLeague.Domain.Entities.Fixtures;
using LegendsLeague.Infrastructure.Persistence.ModelBuilding;
using LegendsLeague.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Infrastructure.Persistence.Fixtures
{
    /// <summary>
    /// EF Core DbContext for the Fixtures bounded context.
    /// Uses the <c>fixtures</c> schema and enforces snake_case naming for all database objects.
    /// </summary>
    public class FixturesDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FixturesDbContext"/>.
        /// </summary>
        /// <param name="options">DbContext options configured by DI.</param>
        public FixturesDbContext(DbContextOptions<FixturesDbContext> options) : base(options) { }

        /// <summary>Series table.</summary>
        public DbSet<Series> Series => Set<Series>();
        /// <summary>Real teams table.</summary>
        public DbSet<RealTeam> RealTeams => Set<RealTeam>();
        /// <summary>Fixtures (matches) table.</summary>
        public DbSet<Fixture> Fixtures => Set<Fixture>();

        /// <summary>
        /// Configures model mappings:
        /// - Sets default schema to <c>fixtures</c>
        /// - Applies entity type configurations from this assembly
        /// - Enforces snake_case naming for all objects
        /// - Applies global soft-delete filter for entities implementing <c>ISoftDeletable</c>
        /// </summary>
        /// <param name="modelBuilder">The EF Core model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("fixtures");

            // Load fluent mappings (tables, relationships, etc.)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FixturesDbContext).Assembly);

            // Enforce snake_case for tables, columns, keys, FKs, and indexes
            modelBuilder.UseSnakeCaseNames();

            // Hide soft-deleted rows by default for types implementing ISoftDeletable
            modelBuilder.ApplySoftDeleteQueryFilters();

            base.OnModelCreating(modelBuilder);
        }
    }
}
