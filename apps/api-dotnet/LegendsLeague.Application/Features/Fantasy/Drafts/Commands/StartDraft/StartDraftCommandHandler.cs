using LegendsLeague.Domain.Abstractions.Persistence;
using DEnums = LegendsLeague.Domain.Entities.Fantasy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.StartDraft;

/// <summary>Handles transitioning a draft from Scheduled to Live.</summary>
public sealed class StartDraftCommandHandler : IRequestHandler<StartDraftCommand, bool>
{
    private readonly IFantasyDbContext _db;

    public StartDraftCommandHandler(IFantasyDbContext db) => _db = db;

    public async Task<bool> Handle(StartDraftCommand request, CancellationToken ct)
    {
        var draft = await _db.Drafts.FirstOrDefaultAsync(d => d.Id == request.DraftId, ct);
        if (draft is null) return false;
        if (draft.Status != DEnums.DraftStatus.Scheduled)
            throw new InvalidOperationException("Only scheduled drafts can be started.");

        draft.Status = DEnums.DraftStatus.Live;
        draft.StartedAtUtc = DateTimeOffset.UtcNow;

        await _db.SaveChangesAsync(ct);
        return true;
    }
}
