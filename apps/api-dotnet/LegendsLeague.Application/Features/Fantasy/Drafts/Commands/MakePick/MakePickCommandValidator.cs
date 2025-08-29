using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.MakePick;

/// <summary>Validation for <see cref="MakePickCommand"/>.</summary>
public sealed class MakePickCommandValidator : AbstractValidator<MakePickCommand>
{
    public MakePickCommandValidator()
    {
        RuleFor(x => x.DraftId).NotEmpty();
        RuleFor(x => x.LeagueTeamId).NotEmpty();
        RuleFor(x => x.PlayerId).NotEmpty();
        RuleFor(x => x.OverallPickNumber).GreaterThan(0);
    }
}
