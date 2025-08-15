using FluentAssertions;
using FluentValidation.TestHelper;
using LegendsLeague.Application.Features.Fixtures.Commands.CreateSeries;
using LegendsLeague.Application.Features.Fixtures.Commands.CreateSeries.Validators;

namespace LegendsLeague.Tests.Unit.Application.Features.Fixtures.Commands.CreateSeries;

/// <summary>
/// Unit tests for <see cref="CreateSeriesCommandValidator"/>.
/// </summary>
public class CreateSeriesCommandValidatorTests
{
    [Fact]
    public void Rejects_empty_or_too_long_name()
    {
        var v = new CreateSeriesCommandValidator();

        v.TestValidate(new CreateSeriesCommand("", 2026))
            .ShouldHaveValidationErrorFor(c => c.Name);

        var longName = new string('X', 201);
        v.TestValidate(new CreateSeriesCommand(longName, 2026))
            .ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Fact]
    public void Rejects_out_of_range_year()
    {
        var v = new CreateSeriesCommandValidator();

        v.TestValidate(new CreateSeriesCommand("IPL", 1999))
            .ShouldHaveValidationErrorFor(c => c.SeasonYear);

        v.TestValidate(new CreateSeriesCommand("IPL", 2101))
            .ShouldHaveValidationErrorFor(c => c.SeasonYear);
    }

    [Fact]
    public void Accepts_valid_payload()
    {
        var v = new CreateSeriesCommandValidator();

        v.TestValidate(new CreateSeriesCommand("Indian Premier League", 2026))
            .IsValid.Should().BeTrue();
    }
}
