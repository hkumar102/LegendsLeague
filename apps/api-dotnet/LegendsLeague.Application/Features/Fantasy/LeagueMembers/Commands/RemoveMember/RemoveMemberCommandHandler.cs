
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Commands.RemoveMember;

/// <summary>Handles removing a league member.</summary>
public sealed class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand, bool>
{
    private readonly IFantasyDbContext _db;

    public RemoveMemberCommandHandler(IFantasyDbContext db) => _db = db;

    public async Task<bool> Handle(RemoveMemberCommand request, CancellationToken ct)
    {
        var member = await _db.LeagueMembers.FirstOrDefaultAsync(
            m => m.Id == request.MemberId && m.LeagueId == request.LeagueId, ct);

        if (member is null) return false;

        _db.LeagueMembers.Remove(member);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
