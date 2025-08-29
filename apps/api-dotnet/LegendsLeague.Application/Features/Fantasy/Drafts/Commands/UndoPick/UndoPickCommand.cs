using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.UndoPick;

/// <summary>Undo and remove a previously completed pick.</summary>
public sealed record UndoPickCommand(
    Guid DraftId,
    int OverallPickNumber
) : IRequest<bool>;
