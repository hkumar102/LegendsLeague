using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Queries.Validators;

/// <summary>Validation rules for <see cref="SearchLeaguesQuery"/>.</summary>
public sealed class SearchLeaguesQueryValidator : AbstractValidator<SearchLeaguesQuery>
{
    public SearchLeaguesQueryValidator()
    {
        RuleFor(x => x.Search).NotEmpty().MinimumLength(2).MaximumLength(120);
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}
