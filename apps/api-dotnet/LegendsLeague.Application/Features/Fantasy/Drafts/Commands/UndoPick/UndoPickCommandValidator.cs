using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.UndoPick;

/// <summary>Validation for <see cref="UndoPickCommand"/>.</summary>
public sealed class UndoPickCommandValidator : AbstractValidator<UndoPickCommand>
{
    public UndoPickCommandValidator()
    {
        RuleFor(x => x.DraftId).NotEmpty();
        RuleFor(x => x.OverallPickNumber).GreaterThan(0);
    }
}
