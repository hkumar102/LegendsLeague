using AutoMapper;
using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.LeagueTeams.Queries;

/// <summary>Handles SearchLeagueTeamsQuery (paginated).</summary>
public sealed class SearchLeagueTeamsQueryHandler
    : IRequestHandler<SearchLeagueTeamsQuery, PaginatedResult<LeagueTeamDto>>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public SearchLeagueTeamsQueryHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<LeagueTeamDto>> Handle(SearchLeagueTeamsQuery request, CancellationToken ct)
    {
        var term = request.Search.Trim().ToLower();
        var q = _db.LeagueTeams.AsNoTracking().AsQueryable();

        if (request.LeagueId.HasValue)
            q = q.Where(t => t.LeagueId == request.LeagueId.Value);

        q = q.Where(t => t.Name.ToLower().Contains(term))
             .OrderBy(t => t.Name);

        return await q.ToPaginatedResultAsync<Domain.Entities.Fantasy.LeagueTeam, LeagueTeamDto>(
            request.Page, request.PageSize, _mapper.ConfigurationProvider, ct: ct);
    }
}
