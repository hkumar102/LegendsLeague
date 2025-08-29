using AutoMapper;
using C = LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Application.Common.Mapping;
using LegendsLeague.Domain.Abstractions.Persistence;
using LegendsLeague.Domain.Entities.Fantasy;
using DEnums = LegendsLeague.Domain.Entities.Fantasy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Commands.CreateDraft;

/// <summary>Handles creating a draft under a league.</summary>
public sealed class CreateDraftCommandHandler : IRequestHandler<CreateDraftCommand, C.DraftDto>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public CreateDraftCommandHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<C.DraftDto> Handle(CreateDraftCommand request, CancellationToken ct)
    {
        var leagueExists = await _db.Leagues.AsNoTracking().AnyAsync(l => l.Id == request.LeagueId, ct);
        if (!leagueExists) throw new KeyNotFoundException("League not found.");

        var draft = new Draft
        {
            Id = Guid.NewGuid(),
            LeagueId = request.LeagueId,
            DraftType = request.DraftType.ToDomain(), // Contracts -> Domain
            Status = DEnums.DraftStatus.Scheduled,
            ScheduledAtUtc = request.ScheduledAtUtc
        };

        await _db.Drafts.AddAsync(draft, ct);
        await _db.SaveChangesAsync(ct);

        return _mapper.Map<C.DraftDto>(draft);
    }
}
