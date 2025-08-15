using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Commands.UpdateTeam.Validators;

/// <summary>
/// Validation rules for <see cref="UpdateTeamCommand"/>.
/// </summary>
public sealed class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
{
    /// <summary>
    /// Initializes validation for updating a team.
    /// </summary>
    public UpdateTeamCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(120);

        RuleFor(x => x.ShortName)
            .MaximumLength(10)
            .When(x => x.ShortName != null);
    }
}
