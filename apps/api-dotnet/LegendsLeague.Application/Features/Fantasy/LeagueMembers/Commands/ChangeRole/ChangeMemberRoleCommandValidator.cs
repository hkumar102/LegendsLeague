using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Commands.ChangeRole;

/// <summary>Validation for ChangeMemberRoleCommand.</summary>
public sealed class ChangeMemberRoleCommandValidator : AbstractValidator<ChangeMemberRoleCommand>
{
    public ChangeMemberRoleCommandValidator()
    {
        RuleFor(x => x.LeagueId).NotEmpty();
        RuleFor(x => x.MemberId).NotEmpty();
        RuleFor(x => x.NewRole).IsInEnum();
    }
}
