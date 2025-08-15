using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Players.Queries.Validators;

public sealed class SearchPlayersQueryValidator : AbstractValidator<SearchPlayersQuery>
{
    public SearchPlayersQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}
