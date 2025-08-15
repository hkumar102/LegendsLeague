using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Fixtures.Commands.UpdateFixture.Validators;

public sealed class UpdateFixtureCommandValidator : AbstractValidator<UpdateFixtureCommand>
{
    public UpdateFixtureCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x).Must(x => !(x.HomeTeamId.HasValue && x.AwayTeamId.HasValue && x.HomeTeamId == x.AwayTeamId))
            .WithMessage("HomeTeamId must be different from AwayTeamId.");
    }
}
