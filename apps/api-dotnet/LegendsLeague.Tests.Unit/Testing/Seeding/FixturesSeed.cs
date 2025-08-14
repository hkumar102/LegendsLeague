using LegendsLeague.Domain.Entities.Fixtures;

namespace LegendsLeague.Tests.Unit.Testing.Seeding;

/// <summary>
/// Provides seed data objects for tests.
/// </summary>
public static class FixturesSeed
{
    /// <summary>
    /// Returns a set of sample series for tests.
    /// </summary>
    public static List<Series> Series() => new()
    {
        new Series { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Indian Premier League", SeasonYear = 2025 },
        new Series { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Indian Premier League", SeasonYear = 2026 },
        new Series { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "ICC T20 World Cup",    SeasonYear = 2026 },
        new Series { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Big Bash League",       SeasonYear = 2024 },
        new Series { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Caribbean Premier League", SeasonYear = 2026 },
    };
}
