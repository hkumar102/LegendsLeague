using AutoMapper;

using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Queries;

/// <summary>
/// Handles paginated listing of leagues by series.
/// </summary>
public sealed class GetLeaguesBySeriesQueryHandler
    : IRequestHandler<GetLeaguesBySeriesQuery, PaginatedResult<FantasyLeagueDto>>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public GetLeaguesBySeriesQueryHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<FantasyLeagueDto>> Handle(GetLeaguesBySeriesQuery request, CancellationToken ct)
    {
        IQueryable<LegendsLeague.Domain.Entities.Fantasy.FantasyLeague> q =
            _db.Leagues.AsNoTracking().Where(l => l.SeriesId == request.SeriesId);

        q = request.Sort?.Trim() switch
        {
            "-name" => q.OrderByDescending(l => l.Name),
            _       => q.OrderBy(l => l.Name)
        };

        return await q.ToPaginatedResultAsync<LegendsLeague.Domain.Entities.Fantasy.FantasyLeague, FantasyLeagueDto>(
            request.Page, request.PageSize, _mapper.ConfigurationProvider, ct: ct);
    }
}
