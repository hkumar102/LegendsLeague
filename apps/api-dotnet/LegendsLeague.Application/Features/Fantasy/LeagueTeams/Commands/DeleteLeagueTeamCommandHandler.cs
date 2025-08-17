using LegendsLeague.Application.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Commands;

/// <summary>Handles DeleteLeagueTeamCommand.</summary>
public sealed class DeleteLeagueTeamCommandHandler : IRequestHandler<DeleteLeagueTeamCommand, bool>
{
    private readonly IFantasyDbContext _db;

    public DeleteLeagueTeamCommandHandler(IFantasyDbContext db) => _db = db;

    public async Task<bool> Handle(DeleteLeagueTeamCommand request, CancellationToken ct)
    {
        var team = await _db.LeagueTeams.FirstOrDefaultAsync(t => t.Id == request.Id, ct);
        if (team is null) return false;

        _db.LeagueTeams.Remove(team);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
