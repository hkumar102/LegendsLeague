using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Commands.CreateTeam.Validators;

/// <summary>
/// Validation rules for <see cref="CreateTeamCommand"/>.
/// </summary>
public sealed class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    /// <summary>
    /// Initializes validation for creating a team.
    /// </summary>
    public CreateTeamCommandValidator()
    {
        RuleFor(x => x.SeriesId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(120);

        RuleFor(x => x.ShortName)
            .MaximumLength(10)
            .When(x => x.ShortName != null);
    }
}
