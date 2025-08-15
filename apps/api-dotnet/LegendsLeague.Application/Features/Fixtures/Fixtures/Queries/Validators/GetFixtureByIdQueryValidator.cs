using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Fixtures.Queries.Validators;

public sealed class GetFixtureByIdQueryValidator : AbstractValidator<GetFixtureByIdQuery>
{
    public GetFixtureByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
