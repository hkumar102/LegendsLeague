using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.StartDraft;

/// <summary>Validation for <see cref="StartDraftCommand"/>.</summary>
public sealed class StartDraftCommandValidator : AbstractValidator<StartDraftCommand>
{
    public StartDraftCommandValidator()
    {
        RuleFor(x => x.DraftId).NotEmpty();
    }
}
