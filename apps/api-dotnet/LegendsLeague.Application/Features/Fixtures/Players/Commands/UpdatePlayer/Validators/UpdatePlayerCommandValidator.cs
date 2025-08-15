using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Players.Commands.UpdatePlayer.Validators;

public sealed class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FullName).MaximumLength(160).When(x => x.FullName is not null);
        RuleFor(x => x.ShortName).MaximumLength(60).When(x => x.ShortName is not null);
        RuleFor(x => x.Country).MaximumLength(80).When(x => x.Country is not null);
    }
}
