using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Queries.Validators;

/// <summary>
/// Validation rules for <c>GetSeriesListQuery</c>.
/// </summary>
public sealed class GetSeriesListQueryValidator : AbstractValidator<GetSeriesListQuery>
{
    /// <summary>
    /// Initializes a new instance of the validator with standard paging, sorting, and year bounds.
    /// </summary>
    public GetSeriesListQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);

        RuleFor(x => x.SeasonYear)
            .InclusiveBetween(2000, 2100)
            .When(x => x.SeasonYear.HasValue);

        RuleFor(x => x.Sort)
            .Must(k => string.IsNullOrWhiteSpace(k) || IsAllowedSort(k!))
            .WithMessage("Sort must be one of: name, -name, seasonYear, -seasonYear.");
    }

    private static bool IsAllowedSort(string key)
        => key is "name" or "-name" or "seasonYear" or "-seasonYear";
}
