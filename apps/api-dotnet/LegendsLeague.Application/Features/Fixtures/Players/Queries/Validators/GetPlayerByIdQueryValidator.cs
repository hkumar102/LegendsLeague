using FluentValidation;

namespace LegendsLeague.Application.Features.Fixtures.Players.Queries.Validators;

public sealed class GetPlayerByIdQueryValidator : AbstractValidator<GetPlayerByIdQuery>
{
    public GetPlayerByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
