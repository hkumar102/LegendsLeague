using AutoMapper;
using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Queries;

/// <summary>
/// Handles paginated, case-insensitive league name search.
/// Uses ToLower().Contains() for test-provider portability.
/// </summary>
public sealed class SearchLeaguesQueryHandler
    : IRequestHandler<SearchLeaguesQuery, PaginatedResult<FantasyLeagueDto>>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public SearchLeaguesQueryHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<FantasyLeagueDto>> Handle(SearchLeaguesQuery request, CancellationToken ct)
    {
        var term = request.Search.Trim().ToLower();

        IQueryable<LegendsLeague.Domain.Entities.Fantasy.FantasyLeague> q = _db.Leagues.AsNoTracking();

        if (request.SeriesId.HasValue)
            q = q.Where(l => l.SeriesId == request.SeriesId.Value);

        q = q.Where(l => l.Name.ToLower().Contains(term))
             .OrderBy(l => l.Name);

        return await q.ToPaginatedResultAsync<Domain.Entities.Fantasy.FantasyLeague, FantasyLeagueDto>(
            request.Page, request.PageSize, _mapper.ConfigurationProvider, ct: ct);
    }
}
