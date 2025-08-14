using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Domain.Entities.Fixtures;
using LegendsLeague.Infrastructure.Persistence.Extensions;
using LegendsLeague.Infrastructure.Persistence.ModelBuilding;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Infrastructure.Persistence.Fixtures
{
    /// <summary>
    /// EF Core DbContext for the Fixtures bounded context.
    /// Uses the <c>fixtures</c> schema and enforces snake_case naming for all database objects.
    /// Implements <see cref="IFixturesDbContext"/> so the Application layer can depend on an abstraction.
    /// </summary>
    public class FixturesDbContext : DbContext, IFixturesDbContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FixturesDbContext"/>.
        /// </summary>
        /// <param name="options">DbContext options configured by DI.</param>
        public FixturesDbContext(DbContextOptions<FixturesDbContext> options) : base(options) { }

        /// <summary>Series table (e.g., IPL 2026).</summary>
        public DbSet<Series> Series => Set<Series>();

        /// <summary>Real teams participating in a series.</summary>
        public DbSet<RealTeam> RealTeams => Set<RealTeam>();

        /// <summary>Scheduled fixtures (matches) within a series.</summary>
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

        // NOTE: IFixturesDbContext requires SaveChangesAsync. DbContext already provides it,
        // so no override is necessary unless you want to customize behavior.
        // public override Task<int> SaveChangesAsync(CancellationToken ct = default) => base.SaveChangesAsync(ct);
    }
}
