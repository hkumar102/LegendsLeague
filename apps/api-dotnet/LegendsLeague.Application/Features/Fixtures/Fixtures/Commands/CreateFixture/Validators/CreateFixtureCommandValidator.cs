using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Fixtures.Commands.CreateFixture.Validators;

public sealed class CreateFixtureCommandValidator : AbstractValidator<CreateFixtureCommand>
{
    public CreateFixtureCommandValidator()
    {
        RuleFor(x => x.SeriesId).NotEmpty();
        RuleFor(x => x.HomeTeamId).NotEmpty();
        RuleFor(x => x.AwayTeamId).NotEmpty()
            .NotEqual(x => x.HomeTeamId).WithMessage("AwayTeamId must be different from HomeTeamId.");
        RuleFor(x => x.StartTimeUtc).NotEmpty();
    }
}
