using AutoMapper;
using C = LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using LegendsLeague.Domain.Entities.Fantasy;
using DEnums = LegendsLeague.Domain.Entities.Fantasy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.MakePick;

/// <summary>Handles making a pick during a live draft (minimal rules).</summary>
public sealed class MakePickCommandHandler : IRequestHandler<MakePickCommand, C.DraftPickDto>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public MakePickCommandHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<C.DraftPickDto> Handle(MakePickCommand request, CancellationToken ct)
    {
        var draft = await _db.Drafts.AsNoTracking().FirstOrDefaultAsync(d => d.Id == request.DraftId, ct);
        if (draft is null) throw new KeyNotFoundException("Draft not found.");
        if (draft.Status != DEnums.DraftStatus.Live)
            throw new InvalidOperationException("Picks can only be made in a live draft.");

        var exists = await _db.DraftPicks.AsNoTracking()
            .AnyAsync(p => p.DraftId == request.DraftId && p.OverallPickNumber == request.OverallPickNumber, ct);
        if (exists) throw new InvalidOperationException("Overall pick number already used.");

        var playerTaken = await _db.DraftPicks.AsNoTracking()
            .AnyAsync(p => p.DraftId == request.DraftId && p.PlayerId == request.PlayerId, ct);
        if (playerTaken) throw new InvalidOperationException("Player already drafted.");

        var pick = new DraftPick
        {
            Id = Guid.NewGuid(),
            DraftId = request.DraftId,
            LeagueTeamId = request.LeagueTeamId,
            PlayerId = request.PlayerId,
            OverallPickNumber = request.OverallPickNumber,
            Status = DEnums.DraftPickStatus.Completed,
            PickedAtUtc = DateTimeOffset.UtcNow
        };

        await _db.DraftPicks.AddAsync(pick, ct);
        await _db.SaveChangesAsync(ct);

        return _mapper.Map<C.DraftPickDto>(pick);
    }
}
