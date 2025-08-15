using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Series.Queries.Validators;

public sealed class GetSeriesListQueryValidator : AbstractValidator<GetSeriesListQuery>
{
    public GetSeriesListQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        RuleFor(x => x.SeasonYear).InclusiveBetween(2000, 2100).When(x => x.SeasonYear.HasValue);
        RuleFor(x => x.Sort)
            .Must(k => string.IsNullOrWhiteSpace(k) || k is "name" or "-name" or "seasonYear" or "-seasonYear")
            .WithMessage("Sort must be one of: name, -name, seasonYear, -seasonYear.");
    }
}
