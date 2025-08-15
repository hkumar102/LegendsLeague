using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Players.Commands.DeletePlayer.Validators;

public sealed class DeletePlayerCommandValidator : AbstractValidator<DeletePlayerCommand>
{
    public DeletePlayerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
