using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Commands.UpdateLeague;

public sealed class UpdateLeagueCommandValidator : AbstractValidator<UpdateLeagueCommand>
{
    public UpdateLeagueCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        When(x => x.Name is not null, () =>
        {
            RuleFor(x => x.Name!)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(120);
        });

        When(x => x.MaxTeams.HasValue, () =>
        {
            RuleFor(x => x.MaxTeams!.Value)
                .InclusiveBetween(2, 20);
        });
    }
}
