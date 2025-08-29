using FluentValidation;
using LegendsLeague.Contracts.Fantasy;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.UpdateDraft;

/// <summary>Validation for <see cref="UpdateDraftCommand"/>.</summary>
public sealed class UpdateDraftCommandValidator : AbstractValidator<UpdateDraftCommand>
{
    public UpdateDraftCommandValidator()
    {
        RuleFor(x => x.DraftId).NotEmpty();
        RuleFor(x => x.DraftType).IsInEnum();
        RuleFor(x => x.ScheduledAtUtc).GreaterThan(DateTimeOffset.UtcNow.AddMinutes(-1));
    }
}
