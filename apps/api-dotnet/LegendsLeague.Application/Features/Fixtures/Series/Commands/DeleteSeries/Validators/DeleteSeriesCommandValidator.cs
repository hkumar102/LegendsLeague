using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Series.Commands.DeleteSeries.Validators;

/// <summary>
/// Validation rules for deleting a series.
/// </summary>
public sealed class DeleteSeriesCommandValidator : AbstractValidator<DeleteSeriesCommand>
{
    public DeleteSeriesCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
