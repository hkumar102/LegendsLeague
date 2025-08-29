using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.CreateDraft;

/// <summary>Create a draft for a league.</summary>
public sealed record CreateDraftCommand(
    Guid LeagueId,
    DraftType DraftType,
    DateTimeOffset ScheduledAtUtc
) : IRequest<DraftDto>;
