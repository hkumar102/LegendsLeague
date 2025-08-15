using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Queries.Validators;

/// <summary>Validation rules for <see cref="GetLeagueByIdQuery"/>.</summary>
public sealed class GetLeagueByIdQueryValidator : AbstractValidator<GetLeagueByIdQuery>
{
    public GetLeagueByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
