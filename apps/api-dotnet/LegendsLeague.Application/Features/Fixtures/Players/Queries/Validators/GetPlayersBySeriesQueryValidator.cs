using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Players.Queries.Validators;

public sealed class GetPlayersBySeriesQueryValidator : AbstractValidator<GetPlayersBySeriesQuery>
{
    public GetPlayersBySeriesQueryValidator()
    {
        RuleFor(x => x.SeriesId).NotEmpty();
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}
