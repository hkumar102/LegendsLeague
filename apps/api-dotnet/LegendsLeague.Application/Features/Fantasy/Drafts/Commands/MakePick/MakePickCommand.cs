using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.MakePick;

/// <summary>Make a pick in a live draft.</summary>
public sealed record MakePickCommand(
    Guid DraftId,
    Guid LeagueTeamId,
    Guid PlayerId,
    int OverallPickNumber
) : IRequest<DraftPickDto>;
