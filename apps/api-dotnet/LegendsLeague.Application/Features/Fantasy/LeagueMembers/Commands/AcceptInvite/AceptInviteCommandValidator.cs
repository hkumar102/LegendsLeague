using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Commands.AcceptInvite;

/// <summary>Validation for AcceptInviteCommand.</summary>
public sealed class AcceptInviteCommandValidator : AbstractValidator<AcceptInviteCommand>
{
    public AcceptInviteCommandValidator()
    {
        RuleFor(x => x.LeagueId).NotEmpty();
        RuleFor(x => x.MemberId).NotEmpty();
    }
}
