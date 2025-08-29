using AutoMapper;

using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Teams;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Queries;

/// <summary>
/// Query to retrieve a paged, filtered, and sorted list of real teams within a specific series.
/// </summary>
public sealed record GetTeamsBySeriesQuery(
    Guid SeriesId,
    string? Search = null,
    int Page = 1,
    int PageSize = 20,
    string? Sort = null
) : IRequest<PaginatedResult<RealTeamDto>>;

public sealed class GetTeamsBySeriesQueryHandler
    : IRequestHandler<GetTeamsBySeriesQuery, PaginatedResult<RealTeamDto>>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public GetTeamsBySeriesQueryHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<RealTeamDto>> Handle(GetTeamsBySeriesQuery request, CancellationToken ct)
    {
        var q = _db.RealTeams.AsNoTracking().Where(t => t.SeriesId == request.SeriesId);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim().ToLower();
            q = q.Where(t =>
                EF.Functions.ILike(t.Name, $"%{term}%") ||
                (t.ShortName != null && EF.Functions.ILike(t.ShortName, $"%{term}%")));
        }

        q = request.Sort?.Trim() switch
        {
            "name"        => q.OrderBy(t => t.Name),
            "-name"       => q.OrderByDescending(t => t.Name),
            "shortName"   => q.OrderBy(t => t.ShortName).ThenBy(t => t.Name),
            "-shortName"  => q.OrderByDescending(t => t.ShortName).ThenByDescending(t => t.Name),
            _             => q.OrderBy(t => t.Name)
        };

        return await q.ToPaginatedResultAsync<LegendsLeague.Domain.Entities.Fixtures.RealTeam, RealTeamDto>(
            request.Page,
            request.PageSize,
            _mapper.ConfigurationProvider,
            request.Sort,
            ct);
    }
}
