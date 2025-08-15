using LegendsLeague.Application.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Commands.DeleteLeague;

public sealed class DeleteLeagueCommandHandler : IRequestHandler<DeleteLeagueCommand, bool>
{
    private readonly IFantasyDbContext _db;

    public DeleteLeagueCommandHandler(IFantasyDbContext db) => _db = db;

    public async Task<bool> Handle(DeleteLeagueCommand request, CancellationToken ct)
    {
        var league = await _db.Leagues.FirstOrDefaultAsync(l => l.Id == request.Id, ct);
        if (league is null) return false;

        _db.Leagues.Remove(league);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
