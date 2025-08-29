using AutoMapper;

using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using LegendsLeague.Domain.Entities.Fantasy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Commands;

/// <summary>Handles CreateLeagueTeamCommand.</summary>
public sealed class CreateLeagueTeamCommandHandler : IRequestHandler<CreateLeagueTeamCommand, LeagueTeamDto>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public CreateLeagueTeamCommandHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<LeagueTeamDto> Handle(CreateLeagueTeamCommand request, CancellationToken ct)
    {
        // Optional: ensure league exists
        var leagueExists = await _db.Leagues.AsNoTracking().AnyAsync(l => l.Id == request.LeagueId, ct);
        if (!leagueExists) throw new KeyNotFoundException("League not found.");

        // Name uniqueness per league (case-insensitive)
        var dup = await _db.LeagueTeams.AsNoTracking()
            .AnyAsync(t => t.LeagueId == request.LeagueId && t.Name.ToLower() == request.Name.ToLower(), ct);
        if (dup) throw new InvalidOperationException("A team with the same name already exists in this league.");

        var team = new LeagueTeam
        {
            Id = Guid.NewGuid(),
            LeagueId = request.LeagueId,
            Name = request.Name.Trim(),
            OwnerId = request.OwnerUserId
        };

        await _db.LeagueTeams.AddAsync(team, ct);
        await _db.SaveChangesAsync(ct);

        return _mapper.Map<LeagueTeamDto>(team);
    }
}
