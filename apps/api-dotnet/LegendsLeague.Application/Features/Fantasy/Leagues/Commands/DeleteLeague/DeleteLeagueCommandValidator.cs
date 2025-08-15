using FluentValidation;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Commands.DeleteLeague;

public sealed class DeleteLeagueCommandValidator : AbstractValidator<DeleteLeagueCommand>
{
    public DeleteLeagueCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
