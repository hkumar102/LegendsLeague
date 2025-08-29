using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Commands;

/// <summary>Validation rules for creating a league team.</summary>
public sealed class CreateLeagueTeamCommandValidator : AbstractValidator<CreateLeagueTeamCommand>
{
    public CreateLeagueTeamCommandValidator()
    {
        RuleFor(x => x.LeagueId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(80);
        RuleFor(x => x.OwnerUserId).NotEmpty();
    }
}
