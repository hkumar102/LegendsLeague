using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Queries.Validators;

/// <summary>
/// Validation rules for <c>GetSeriesByIdQuery</c>.
/// </summary>
public sealed class GetSeriesByIdQueryValidator : AbstractValidator<GetSeriesByIdQuery>
{
    /// <summary>
    /// Ensures the identifier is not empty.
    /// </summary>
    public GetSeriesByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
