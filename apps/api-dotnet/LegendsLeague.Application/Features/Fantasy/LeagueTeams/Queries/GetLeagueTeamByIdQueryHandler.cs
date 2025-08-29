using AutoMapper;
using AutoMapper.QueryableExtensions;

using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Queries;

/// <summary>Handles GetLeagueTeamByIdQuery.</summary>
public sealed class GetLeagueTeamByIdQueryHandler : IRequestHandler<GetLeagueTeamByIdQuery, LeagueTeamDto?>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public GetLeagueTeamByIdQueryHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<LeagueTeamDto?> Handle(GetLeagueTeamByIdQuery request, CancellationToken ct)
    {
        return await _db.LeagueTeams.AsNoTracking()
            .Where(t => t.Id == request.Id)
            .ProjectTo<LeagueTeamDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);
    }
}
