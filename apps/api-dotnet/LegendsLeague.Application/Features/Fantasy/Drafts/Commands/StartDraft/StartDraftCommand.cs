using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.StartDraft;

/// <summary>Start a scheduled draft.</summary>
public sealed record StartDraftCommand(Guid DraftId) : IRequest<bool>;
