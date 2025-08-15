using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Domain.Entities.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Tests.Unit.Testing.Fakes;

/// <summary>
/// An EF Core InMemory-based DbContext that implements <see cref="IFixturesDbContext"/>
/// so Application handlers can be tested without a real database.
/// </summary>
public sealed class FakeFixturesDbContext : DbContext, IFixturesDbContext
{
    /// <summary>Series set.</summary>
    public DbSet<Series> Series => Set<Series>();
    /// <summary>Real teams set.</summary>
    public DbSet<RealTeam> RealTeams => Set<RealTeam>();
    /// <summary>Fixtures (matches) set.</summary>
    public DbSet<Fixture> Fixtures => Set<Fixture>();
    /// <summary>Players set.</summary>
    public DbSet<Player> Players => Set<Player>();

    /// <summary>
    /// Creates a new in-memory context instance with an optional unique database name.
    /// </summary>
    /// <param name="dbName">Optional unique database name (use a GUID per test to isolate state).</param>
    public static FakeFixturesDbContext Create(string? dbName = null)
    {
        dbName ??= $"fixtures-tests-{Guid.NewGuid()}";
        var options = new DbContextOptionsBuilder<FakeFixturesDbContext>()
            .UseInMemoryDatabase(dbName)
            .EnableSensitiveDataLogging()
            .Options;
        var ctx = new FakeFixturesDbContext(options);
        return ctx;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FakeFixturesDbContext"/> class.
    /// </summary>
    /// <param name="options">DbContext options.</param>
    public FakeFixturesDbContext(DbContextOptions<FakeFixturesDbContext> options) : base(options) { }
}
