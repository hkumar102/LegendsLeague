using FluentAssertions;
using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Features.Fixtures.Queries;
using LegendsLeague.Tests.Unit.Testing.Fakes;
using LegendsLeague.Tests.Unit.Testing.Seeding;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Tests.Unit.Application.Features.Fixtures.Queries;

/// <summary>
/// Unit tests for <see cref="GetSeriesByIdQueryHandler"/>.
/// </summary>
public class GetSeriesByIdQueryHandlerTests
{
    [Fact]
    public async Task Returns_series_when_found()
    {
        using var ctx = FakeFixturesDbContext.Create();
        await SeedAsync(ctx);

        var existing = FixturesSeed.Series().First().Id;
        var handler = new GetSeriesByIdQueryHandler(ctx);

        var dto = await handler.Handle(new GetSeriesByIdQuery(existing), default);

        dto.Should().NotBeNull();
        dto!.Id.Should().Be(existing);
    }

    [Fact]
    public async Task Returns_null_when_not_found()
    {
        using var ctx = FakeFixturesDbContext.Create();
        await SeedAsync(ctx);

        var handler = new GetSeriesByIdQueryHandler(ctx);
        var dto = await handler.Handle(new GetSeriesByIdQuery(Guid.NewGuid()), default);

        dto.Should().BeNull();
    }

    private static async Task SeedAsync(IFixturesDbContext db)
    {
        await db.Series.AddRangeAsync(FixturesSeed.Series());
        await db.SaveChangesAsync();
        if (db is DbContext ef) ef.ChangeTracker.Clear();
    }
}
