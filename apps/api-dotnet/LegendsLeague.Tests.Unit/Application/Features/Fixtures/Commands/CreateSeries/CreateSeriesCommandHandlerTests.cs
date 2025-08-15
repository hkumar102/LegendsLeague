using FluentAssertions;
using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Application.Features.Fixtures.Commands.CreateSeries;
using LegendsLeague.Domain.Entities.Fixtures;
using LegendsLeague.Tests.Unit.Testing.Fakes;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Tests.Unit.Application.Features.Fixtures.Commands.CreateSeries;

/// <summary>
/// Unit tests for <see cref="CreateSeriesCommandHandler"/>.
/// </summary>
public class CreateSeriesCommandHandlerTests
{
    [Fact]
    public async Task Creates_series_when_not_duplicate()
    {
        using var ctx = FakeFixturesDbContext.Create();
        // No seed; ensure empty for this test

        var handler = new CreateSeriesCommandHandler(ctx);
        var dto = await handler.Handle(new CreateSeriesCommand("Indian Premier League", 2026), default);

        dto.Should().NotBeNull();
        dto.Name.Should().Be("Indian Premier League");
        dto.SeasonYear.Should().Be(2026);

        // Verify it persisted
        var persisted = await ctx.Series.AsNoTracking().FirstOrDefaultAsync(s => s.Id == dto.Id);
        persisted.Should().NotBeNull();
    }

    [Fact]
    public async Task Throws_conflict_when_duplicate_name_year_case_insensitive()
    {
        using var ctx = FakeFixturesDbContext.Create();
        ctx.Series.Add(new Series { Id = Guid.NewGuid(), Name = "Indian Premier League", SeasonYear = 2026 });
        await ctx.SaveChangesAsync();

        var handler = new CreateSeriesCommandHandler(ctx);

        var act = async () => await handler.Handle(new CreateSeriesCommand("indian premier league", 2026), default);
        await act.Should().ThrowAsync<ConflictException>()
            .WithMessage("*already exists*");
    }
}
