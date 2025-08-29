using AutoMapper;

using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Commands;

/// <summary>Handles UpdateLeagueTeamCommand.</summary>
public sealed class UpdateLeagueTeamCommandHandler : IRequestHandler<UpdateLeagueTeamCommand, LeagueTeamDto>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public UpdateLeagueTeamCommandHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<LeagueTeamDto> Handle(UpdateLeagueTeamCommand request, CancellationToken ct)
    {
        var team = await _db.LeagueTeams.FirstOrDefaultAsync(t => t.Id == request.Id, ct);
        if (team is null) throw new KeyNotFoundException("Team not found.");

        if (!string.IsNullOrWhiteSpace(request.Name) && !request.Name.Equals(team.Name, StringComparison.OrdinalIgnoreCase))
        {
            var dup = await _db.LeagueTeams.AsNoTracking()
                .AnyAsync(t => t.LeagueId == team.LeagueId &&
                               t.Name.ToLower() == request.Name!.ToLower() &&
                               t.Id != team.Id, ct);
            if (dup) throw new InvalidOperationException("Another team with this name already exists in this league.");

            team.Name = request.Name!.Trim();
        }

        if (request.DraftPosition.HasValue)
            team.DraftPosition = request.DraftPosition.Value;

        await _db.SaveChangesAsync(ct);
        return _mapper.Map<LeagueTeamDto>(team);
    }
}
