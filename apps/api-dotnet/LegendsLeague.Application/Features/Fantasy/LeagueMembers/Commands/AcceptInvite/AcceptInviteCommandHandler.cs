
using LegendsLeague.Domain.Abstractions.Persistence;
using LegendsLeague.Domain.Entities.Fantasy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Commands.AcceptInvite;

/// <summary>Handles accepting a league invite.</summary>
public sealed class AcceptInviteCommandHandler : IRequestHandler<AcceptInviteCommand, bool>
{
    private readonly IFantasyDbContext _db;

    public AcceptInviteCommandHandler(IFantasyDbContext db) => _db = db;

    public async Task<bool> Handle(AcceptInviteCommand request, CancellationToken ct)
    {
        var member = await _db.LeagueMembers.FirstOrDefaultAsync(
            m => m.Id == request.MemberId && m.LeagueId == request.LeagueId, ct);

        if (member is null) return false;
        if (member.Status != LeagueMemberStatus.Invited) return false;

        member.Status = LeagueMemberStatus.Active;
        member.JoinedAtUtc = DateTimeOffset.UtcNow;

        await _db.SaveChangesAsync(ct);
        return true;
    }
}
