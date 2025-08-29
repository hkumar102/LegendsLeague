using FluentValidation;
using LegendsLeague.Contracts.Fantasy;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.CreateDraft;

/// <summary>Validation for <see cref="CreateDraftCommand"/>.</summary>
public sealed class CreateDraftCommandValidator : AbstractValidator<CreateDraftCommand>
{
    public CreateDraftCommandValidator()
    {
        RuleFor(x => x.LeagueId).NotEmpty();
        RuleFor(x => x.DraftType).IsInEnum();
        RuleFor(x => x.ScheduledAtUtc)
            .GreaterThan(DateTimeOffset.UtcNow.AddMinutes(-1));
    }
}
