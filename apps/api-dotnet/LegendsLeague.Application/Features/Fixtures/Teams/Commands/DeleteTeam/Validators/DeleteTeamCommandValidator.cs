using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Commands.DeleteTeam.Validators;

/// <summary>
/// Validation rules for <see cref="DeleteTeamCommand"/>.
/// </summary>
public sealed class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
{
    /// <summary>
    /// Ensures the identifier is provided.
    /// </summary>
    public DeleteTeamCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
