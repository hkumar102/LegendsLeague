using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.UndoPick;

/// <summary>Handles undoing a completed pick.</summary>
public sealed class UndoPickCommandHandler : IRequestHandler<UndoPickCommand, bool>
{
    private readonly IFantasyDbContext _db;

    public UndoPickCommandHandler(IFantasyDbContext db) => _db = db;

    public async Task<bool> Handle(UndoPickCommand request, CancellationToken ct)
    {
        var pick = await _db.DraftPicks
            .FirstOrDefaultAsync(p => p.DraftId == request.DraftId && p.OverallPickNumber == request.OverallPickNumber, ct);

        if (pick is null) return false;

        _db.DraftPicks.Remove(pick);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
