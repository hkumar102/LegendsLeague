using AutoMapper;

using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using LegendsLeague.Domain.Entities.Fantasy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using LeagueMemberRole = LegendsLeague.Domain.Entities.Fantasy.LeagueMemberRole;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Commands.CreateLeague;

public sealed class CreateLeagueCommandHandler : IRequestHandler<CreateLeagueCommand, FantasyLeagueDto>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public CreateLeagueCommandHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<FantasyLeagueDto> Handle(CreateLeagueCommand request, CancellationToken ct)
    {
        var exists = await _db.Leagues
            .AsNoTracking()
            .AnyAsync(l => l.SeriesId == request.SeriesId &&
                           l.Name.ToLower() == request.Name.ToLower(), ct);

        if (exists)
            throw new InvalidOperationException("A league with the same name already exists for this series.");

        var league = new FantasyLeague
        {
            Id = Guid.NewGuid(),
            SeriesId = request.SeriesId,
            Name = request.Name.Trim(),
            MaxTeams = request.MaxTeams,
            CommissionerId = request.CommissionerUserId
        };

        await _db.Leagues.AddAsync(league, ct);

        var member = new LeagueMember
        {
            Id = Guid.NewGuid(),
            LeagueId = league.Id,
            UserId = request.CommissionerUserId,
            Role = LeagueMemberRole.Commissioner,
            JoinedAtUtc = DateTimeOffset.UtcNow
        };
        await _db.LeagueMembers.AddAsync(member, ct);

        await _db.SaveChangesAsync(ct);

        return _mapper.Map<FantasyLeagueDto>(league);
    }
}
