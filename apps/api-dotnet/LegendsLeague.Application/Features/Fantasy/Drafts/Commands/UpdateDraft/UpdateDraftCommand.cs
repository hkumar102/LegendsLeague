using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.UpdateDraft;

/// <summary>Update draft type or schedule before it starts.</summary>
public sealed record UpdateDraftCommand(
    Guid DraftId,
    DraftType DraftType,
    DateTimeOffset ScheduledAtUtc
) : IRequest<bool>;
