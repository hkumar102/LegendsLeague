using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Commands.CreateSeries.Validators;

/// <summary>
/// Validation rules for <see cref="CreateSeriesCommand"/>.
/// </summary>
public sealed class CreateSeriesCommandValidator : AbstractValidator<CreateSeriesCommand>
{
    /// <summary>
    /// Initializes rules: non-empty name (<= 200 chars) and SeasonYear within reasonable bounds.
    /// </summary>
    public CreateSeriesCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.SeasonYear)
            .InclusiveBetween(2000, 2100);
    }
}
