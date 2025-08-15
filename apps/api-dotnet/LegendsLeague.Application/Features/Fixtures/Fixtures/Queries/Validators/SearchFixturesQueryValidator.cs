using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Fixtures.Queries.Validators;

public sealed class SearchFixturesQueryValidator : AbstractValidator<SearchFixturesQuery>
{
    public SearchFixturesQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        RuleFor(x => x.ToUtc).GreaterThanOrEqualTo(x => x.FromUtc).When(x => x.FromUtc.HasValue && x.ToUtc.HasValue);
    }
}
