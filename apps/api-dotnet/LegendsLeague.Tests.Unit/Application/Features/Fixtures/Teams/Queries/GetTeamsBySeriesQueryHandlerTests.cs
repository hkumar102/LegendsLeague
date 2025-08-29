using FluentAssertions;

using LegendsLeague.Application.Features.Fixtures.Teams.Queries;
using LegendsLeague.Domain.Entities.Fixtures;
using LegendsLeague.Tests.Unit.Testing.Fakes;
using LegendsLeague.Tests.Unit.Testing.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Tests.Unit.Application.Features.Fixtures.Teams.Queries;

public class GetTeamsBySeriesQueryHandlerTests
{
    [Fact]
    public async Task Returns_paginated_teams_for_series()
    {
        using var ctx = FakeFixturesDbContext.Create();
        var mapper = TestMapper.Create();

        // Seed series + teams
        var seriesId = Guid.NewGuid();
        await ctx.Series.AddAsync(new Series { Id = seriesId, Name = "Test League", SeasonYear = 2026 });
        await ctx.RealTeams.AddRangeAsync(
            new RealTeam { Id = Guid.NewGuid(), SeriesId = seriesId, Name = "Alpha", ShortName = "A" },
            new RealTeam { Id = Guid.NewGuid(), SeriesId = seriesId, Name = "Bravo", ShortName = "B" },
            new RealTeam { Id = Guid.NewGuid(), SeriesId = seriesId, Name = "Charlie", ShortName = "C" }
        );
        await ctx.SaveChangesAsync();
        ctx.ChangeTracker.Clear();

        var handler = new GetTeamsBySeriesQueryHandler(ctx, mapper);
        var page = await handler.Handle(new GetTeamsBySeriesQuery(seriesId, PageSize: 2), default);

        page.TotalCount.Should().Be(3);
        page.Items.Should().HaveCount(2);
        page.TotalPages.Should().Be(2);
    }

    [Fact]
    public async Task Applies_search_and_sorting()
    {
        using var ctx = FakeFixturesDbContext.Create();
        var mapper = TestMapper.Create();

        var seriesId = Guid.NewGuid();
        await ctx.Series.AddAsync(new Series { Id = seriesId, Name = "Test League", SeasonYear = 2026 });
        await ctx.RealTeams.AddRangeAsync(
            new RealTeam { Id = Guid.NewGuid(), SeriesId = seriesId, Name = "Zeta", ShortName = "Z" },
            new RealTeam { Id = Guid.NewGuid(), SeriesId = seriesId, Name = "Echo", ShortName = "E" },
            new RealTeam { Id = Guid.NewGuid(), SeriesId = seriesId, Name = "Delta", ShortName = "D" }
        );
        await ctx.SaveChangesAsync();
        ctx.ChangeTracker.Clear();

        var handler = new GetTeamsBySeriesQueryHandler(ctx, mapper);
        var page = await handler.Handle(new GetTeamsBySeriesQuery(seriesId, Search: "e", Sort: "-name"), default);

        page.TotalCount.Should().Be(3); // InMemory can't translate ILIKE, but basic Where string ops still match for seeded data
        page.Items.Select(i => i.Name).Should().BeInDescendingOrder();
    }
}
