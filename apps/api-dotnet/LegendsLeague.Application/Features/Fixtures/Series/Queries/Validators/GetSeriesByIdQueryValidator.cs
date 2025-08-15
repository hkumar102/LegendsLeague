using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Series.Queries.Validators;

public sealed class GetSeriesByIdQueryValidator : AbstractValidator<GetSeriesByIdQuery>
{
    public GetSeriesByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
