using C = LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Application.Common.Mapping;
using LegendsLeague.Domain.Abstractions.Persistence;
using DEnums = LegendsLeague.Domain.Entities.Fantasy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.UpdateDraft;

/// <summary>Handles updating a draft before it starts.</summary>
public sealed class UpdateDraftCommandHandler : IRequestHandler<UpdateDraftCommand, bool>
{
    private readonly IFantasyDbContext _db;

    public UpdateDraftCommandHandler(IFantasyDbContext db) => _db = db;

    public async Task<bool> Handle(UpdateDraftCommand request, CancellationToken ct)
    {
        var draft = await _db.Drafts.FirstOrDefaultAsync(d => d.Id == request.DraftId, ct);
        if (draft is null) return false;
        if (draft.Status != DEnums.DraftStatus.Scheduled)
            throw new InvalidOperationException("Only scheduled drafts can be updated.");

        draft.DraftType = request.DraftType.ToDomain();
        draft.ScheduledAtUtc = request.ScheduledAtUtc;

        await _db.SaveChangesAsync(ct);
        return true;
    }
}
