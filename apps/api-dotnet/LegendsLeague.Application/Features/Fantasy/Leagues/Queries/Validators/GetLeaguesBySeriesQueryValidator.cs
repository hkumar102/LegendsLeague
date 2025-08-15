using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Queries.Validators;

/// <summary>Validation rules for <see cref="GetLeaguesBySeriesQuery"/>.</summary>
public sealed class GetLeaguesBySeriesQueryValidator : AbstractValidator<GetLeaguesBySeriesQuery>
{
    public GetLeaguesBySeriesQueryValidator()
    {
        RuleFor(x => x.SeriesId).NotEmpty();
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        RuleFor(x => x.Sort)
            .Must(s => string.IsNullOrWhiteSpace(s) || s is "name" or "-name")
            .WithMessage("Sort must be one of: name, -name.");
    }
}
