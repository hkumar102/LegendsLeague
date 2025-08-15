using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Fixtures.Commands.DeleteFixture.Validators;

public sealed class DeleteFixtureCommandValidator : AbstractValidator<DeleteFixtureCommand>
{
    public DeleteFixtureCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
