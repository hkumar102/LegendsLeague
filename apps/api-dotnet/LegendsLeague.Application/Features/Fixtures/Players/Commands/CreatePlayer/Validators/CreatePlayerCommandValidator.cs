using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Players.Commands.CreatePlayer.Validators;

public sealed class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerCommandValidator()
    {
        RuleFor(x => x.SeriesId).NotEmpty();
        RuleFor(x => x.RealTeamId).NotEmpty();
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(160);
        RuleFor(x => x.ShortName).MaximumLength(60);
        RuleFor(x => x.Country).MaximumLength(80);
    }
}
