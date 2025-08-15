using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Queries.Validators;

/// <summary>
/// Validation rules for <see cref="GetTeamsBySeriesQuery"/>.
/// </summary>
public sealed class GetTeamsBySeriesQueryValidator : AbstractValidator<GetTeamsBySeriesQuery>
{
    /// <summary>
    /// Initializes standard validation for series id, paging, and sort keys.
    /// </summary>
    public GetTeamsBySeriesQueryValidator()
    {
        RuleFor(x => x.SeriesId)
            .NotEmpty();

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);

        RuleFor(x => x.Sort)
            .Must(s => string.IsNullOrWhiteSpace(s) || IsAllowedSort(s!))
            .WithMessage("Sort must be one of: name, -name, shortName, -shortName.");
    }

    private static bool IsAllowedSort(string key) =>
        key is "name" or "-name" or "shortName" or "-shortName";
}
