
using LegendsLeague.Domain.Abstractions.Persistence;
using LegendsLeague.Domain.Entities.Fantasy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Commands.ChangeRole;

/// <summary>Handles changing a league member's role.</summary>
public sealed class ChangeMemberRoleCommandHandler : IRequestHandler<ChangeMemberRoleCommand, bool>
{
    private readonly IFantasyDbContext _db;

    public ChangeMemberRoleCommandHandler(IFantasyDbContext db) => _db = db;

    public async Task<bool> Handle(ChangeMemberRoleCommand request, CancellationToken ct)
    {
        var member = await _db.LeagueMembers.FirstOrDefaultAsync(
            m => m.Id == request.MemberId && m.LeagueId == request.LeagueId, ct);

        if (member is null) return false;

        member.Role = (LeagueMemberRole)request.NewRole;
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
