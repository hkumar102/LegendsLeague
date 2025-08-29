using LegendsLeague.Domain.Entities.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Domain.Abstractions.Persistence;

/// <summary>
/// Abstraction for the Fixtures persistence surface used by the Application layer.
/// Exposes only the sets and SaveChanges needed by handlers, decoupling Application from EF details.
/// </summary>
public interface IFixturesDbContext
{
    /// <summary>
    /// Real-world competitions (e.g., IPL 2026).
    /// </summary>
    DbSet<Series> Series { get; }

    /// <summary>
    /// Teams participating in a specific <see cref="Series"/>.
    /// </summary>
    DbSet<RealTeam> RealTeams { get; }

    /// <summary>
    /// Scheduled matches within a <see cref="Series"/>.
    /// </summary>
    DbSet<Fixture> Fixtures { get; }

    /// <summary>
    /// Real-world players assigned to teams within a series.
    /// </summary>
    DbSet<Player> Players { get; }

    /// <summary>
    /// Persists pending changes to the database.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Number of affected entries.</returns>
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
