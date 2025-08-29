using AutoMapper;

using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using FantasyDto =  LegendsLeague.Contracts.Fantasy;
using FantasyEntities = LegendsLeague.Domain.Entities.Fantasy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Commands.InviteMember;

/// <summary>Handles inviting a user into a league.</summary>
public sealed class InviteMemberCommandHandler : IRequestHandler<InviteMemberCommand, LeagueMemberDto>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public InviteMemberCommandHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<LeagueMemberDto> Handle(InviteMemberCommand request, CancellationToken ct)
    {
        // Validate league exists
        var leagueExists = await _db.Leagues.AsNoTracking().AnyAsync(l => l.Id == request.LeagueId, ct);
        if (!leagueExists) throw new KeyNotFoundException("League not found.");

        // Unique user per league (pending or active)
        var dup = await _db.LeagueMembers.AsNoTracking()
            .AnyAsync(m => m.LeagueId == request.LeagueId && m.UserId == request.UserId, ct);
        if (dup) throw new InvalidOperationException("User is already invited or a member of this league.");

        var member = new FantasyEntities.LeagueMember
        {
            Id = Guid.NewGuid(),
            LeagueId = request.LeagueId,
            UserId = request.UserId,
            Role = request.Role switch
            {
                LeagueMemberRole.Commissioner   => (FantasyEntities.LeagueMemberRole)FantasyDto.LeagueMemberRole.Commissioner,
                FantasyDto.LeagueMemberRole.CoCommissioner => (FantasyEntities.LeagueMemberRole)FantasyDto.LeagueMemberRole.CoCommissioner,
                _ => (FantasyEntities.LeagueMemberRole)FantasyDto.LeagueMemberRole.Member
            },
            Status = (FantasyEntities.LeagueMemberStatus)FantasyDto.LeagueMemberStatus.Invited,
            InvitedAtUtc = DateTimeOffset.UtcNow
        };

        await _db.LeagueMembers.AddAsync(member, ct);
        await _db.SaveChangesAsync(ct);

        return _mapper.Map<LeagueMemberDto>(member);
    }
}
