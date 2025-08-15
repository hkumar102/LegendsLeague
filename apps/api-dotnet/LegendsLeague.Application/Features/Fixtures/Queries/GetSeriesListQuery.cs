using AutoMapper;
using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Series;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Queries;

/// <summary>
/// Query to retrieve a paged, filtered, and sorted list of cricket series.
/// </summary>
public sealed record GetSeriesListQuery(
    int? SeasonYear = null,
    string? Search = null,
    int Page = 1,
    int PageSize = 20,
    string? Sort = null
) : IRequest<PaginatedResult<SeriesDto>>;

/// <summary>
/// Handles <see cref="GetSeriesListQuery"/> using AutoMapper projection.
/// </summary>
public sealed class GetSeriesListQueryHandler : IRequestHandler<GetSeriesListQuery, PaginatedResult<SeriesDto>>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public GetSeriesListQueryHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<SeriesDto>> Handle(GetSeriesListQuery request, CancellationToken ct)
    {
        IQueryable<LegendsLeague.Domain.Entities.Fixtures.Series> q = _db.Series.AsNoTracking();

        if (request.SeasonYear.HasValue)
            q = q.Where(s => s.SeasonYear == request.SeasonYear.Value);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim().ToLower();
            q = q.Where(s => EF.Functions.ILike(s.Name, $"%{term}%"));
        }

        q = request.Sort?.Trim() switch
        {
            "name"        => q.OrderBy(s => s.Name).ThenBy(s => s.SeasonYear),
            "-name"       => q.OrderByDescending(s => s.Name).ThenByDescending(s => s.SeasonYear),
            "seasonYear"  => q.OrderBy(s => s.SeasonYear).ThenBy(s => s.Name),
            "-seasonYear" => q.OrderByDescending(s => s.SeasonYear).ThenByDescending(s => s.Name),
            _             => q.OrderBy(s => s.SeasonYear).ThenBy(s => s.Name)
        };

        return await q.ToPaginatedResultAsync<LegendsLeague.Domain.Entities.Fixtures.Series, SeriesDto>(
            request.Page,
            request.PageSize,
            _mapper.ConfigurationProvider,
            request.Sort,
            ct);
    }
}
