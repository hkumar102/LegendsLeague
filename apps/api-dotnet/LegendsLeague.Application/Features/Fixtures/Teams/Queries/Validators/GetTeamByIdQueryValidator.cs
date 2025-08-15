using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Queries.Validators;

/// <summary>
/// Validation rules for <see cref="GetTeamByIdQuery"/>.
/// </summary>
public sealed class GetTeamByIdQueryValidator : AbstractValidator<GetTeamByIdQuery>
{
    public GetTeamByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
