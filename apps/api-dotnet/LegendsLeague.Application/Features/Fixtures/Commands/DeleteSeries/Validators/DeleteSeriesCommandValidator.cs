using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Commands.DeleteSeries.Validators;

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
