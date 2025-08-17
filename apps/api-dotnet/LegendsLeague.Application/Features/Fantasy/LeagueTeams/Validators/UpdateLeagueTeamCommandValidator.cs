using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Commands;

/// <summary>Validation rules for updating a league team.</summary>
public sealed class UpdateLeagueTeamCommandValidator : AbstractValidator<UpdateLeagueTeamCommand>
{
    public UpdateLeagueTeamCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        When(x => x.Name is not null, () =>
        {
            RuleFor(x => x.Name!).NotEmpty().MinimumLength(2).MaximumLength(80);
        });
        When(x => x.DraftPosition.HasValue, () =>
        {
            RuleFor(x => x.DraftPosition!.Value).GreaterThanOrEqualTo(1);
        });
    }
}
