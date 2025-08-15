using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Commands.CreateLeague;

public sealed class CreateLeagueCommandValidator : AbstractValidator<CreateLeagueCommand>
{
    public CreateLeagueCommandValidator()
    {
        RuleFor(x => x.SeriesId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(120);
        RuleFor(x => x.MaxTeams).InclusiveBetween(2, 20);
        RuleFor(x => x.CommissionerUserId).NotEmpty();
    }
}
