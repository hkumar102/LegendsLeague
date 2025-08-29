using AutoMapper;

using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Queries;

/// <summary>Handles GetLeagueTeamsByLeagueIdQuery (paginated).</summary>
public sealed class GetLeagueTeamsByLeagueIdQueryHandler
    : IRequestHandler<GetLeagueTeamsByLeagueIdQuery, PaginatedResult<LeagueTeamDto>>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public GetLeagueTeamsByLeagueIdQueryHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<LeagueTeamDto>> Handle(GetLeagueTeamsByLeagueIdQuery request, CancellationToken ct)
    {
        var q = _db.LeagueTeams.AsQueryable().Where(t => t.LeagueId == request.LeagueId);

        q = request.Sort?.Trim() switch
        {
            "-name" => q.OrderByDescending(t => t.Name),
            _       => q.OrderBy(t => t.Name)
        };

        return await q.ToPaginatedResultAsync<Domain.Entities.Fantasy.LeagueTeam, LeagueTeamDto>(
            request.Page, request.PageSize, _mapper.ConfigurationProvider, ct: ct);
    }
}
