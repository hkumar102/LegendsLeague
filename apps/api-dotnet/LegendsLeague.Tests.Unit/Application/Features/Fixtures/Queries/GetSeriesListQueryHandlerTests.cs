using FluentAssertions;
using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Features.Fixtures.Queries;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Tests.Unit.Testing.Fakes;
using LegendsLeague.Tests.Unit.Testing.Seeding;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Tests.Unit.Application.Features.Fixtures.Queries;

/// <summary>
/// Unit tests for <see cref="GetSeriesListQueryHandler"/> covering paging, sorting, and year filter,
/// updated to assert the PaginatedResult envelope.
/// </summary>
public class GetSeriesListQueryHandlerTests
{
    [Fact]
    public async Task Returns_default_sorted_list_when_no_filters()
    {
        using var ctx = FakeFixturesDbContext.Create();
        await SeedAsync(ctx);

        var handler = new GetSeriesListQueryHandler(ctx);
        PaginatedResult<LegendsLeague.Contracts.Series.SeriesDto> result =
            await handler.Handle(new GetSeriesListQuery(), default);

        result.Should().NotBeNull();
        result.Items.Should().HaveCount(5);
        result.TotalCount.Should().Be(5);
        result.Page.Should().Be(1);
        result.PageSize.Should().Be(20);
        // Default sort: SeasonYear asc then Name
        result.Items.Select(x => x.SeasonYear).Should().BeInAscendingOrder();
    }

    [Fact]
    public async Task Applies_year_filter_and_paging()
    {
        using var ctx = FakeFixturesDbContext.Create();
        await SeedAsync(ctx);

        var handler = new GetSeriesListQueryHandler(ctx);
        var result = await handler.Handle(new GetSeriesListQuery(SeasonYear: 2026, Page: 1, PageSize: 2), default);

        result.Items.Should().HaveCount(2);
        result.TotalCount.Should().Be(3); // from seed: 3 rows where SeasonYear=2026
        result.Items.All(x => x.SeasonYear == 2026).Should().BeTrue();
        result.TotalPages.Should().Be(2);
        result.HasNext.Should().BeTrue();
    }

    [Fact]
    public async Task Sorts_by_name_desc_when_requested()
    {
        using var ctx = FakeFixturesDbContext.Create();
        await SeedAsync(ctx);

        var handler = new GetSeriesListQueryHandler(ctx);
        var result = await handler.Handle(new GetSeriesListQuery(Sort: "-name"), default);

        result.Items.Should().HaveCount(5);
        result.Items.Select(x => x.Name).Should().BeInDescendingOrder();
        result.TotalCount.Should().Be(5);
    }

    private static async Task SeedAsync(IFixturesDbContext db)
    {
        // Use tracking here to add and save
        await db.Series.AddRangeAsync(FixturesSeed.Series());
        await db.SaveChangesAsync();
        // Switch to no-tracking for queries if underlying provider honors it
        if (db is DbContext ef) ef.ChangeTracker.Clear();
    }
}
