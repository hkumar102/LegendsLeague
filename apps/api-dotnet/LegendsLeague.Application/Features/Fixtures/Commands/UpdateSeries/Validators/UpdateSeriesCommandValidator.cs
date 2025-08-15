using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Commands.UpdateSeries.Validators;

/// <summary>
/// Validation rules for updating a series.
/// </summary>
public sealed class UpdateSeriesCommandValidator : AbstractValidator<UpdateSeriesCommand>
{
    public UpdateSeriesCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.SeasonYear).InclusiveBetween(2000, 2100);
    }
}
