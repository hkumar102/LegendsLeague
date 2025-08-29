using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Commands.InviteMember;

/// <summary>Validation for InviteMemberCommand.</summary>
public sealed class InviteMemberCommandValidator : AbstractValidator<InviteMemberCommand>
{
    public InviteMemberCommandValidator()
    {
        RuleFor(x => x.LeagueId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Role).IsInEnum();
    }
}
