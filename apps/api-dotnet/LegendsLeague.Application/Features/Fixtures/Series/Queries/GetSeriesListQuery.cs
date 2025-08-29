using AutoMapper;

using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Series;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Series.Queries;

/// <summary>
/// Query to retrieve a paged, filtered, and sorted list of cricket series.
/// </summary>
/// <param name="SeasonYear">Optional exact season year filter (e.g., 2026).</param>
/// <param name="Search">Optional case-insensitive name substring filter.</param>
/// <param name="Page">1-based page number (default 1).</param>
/// <param name="PageSize">Page size (default 20; max enforced by validator).</param>
/// <param name="Sort">Sort key: name, -name, seasonYear, -seasonYear.</param>
public sealed record GetSeriesListQuery(
    int? SeasonYear = null,
    string? Search = null,
    int Page = 1,
    int PageSize = 20,
    string? Sort = null
) : IRequest<PaginatedResult<SeriesDto>>;

/// <summary>
/// Handles <see cref="GetSeriesListQuery"/> and returns a paginated result.
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
            var term = request.Search.Trim();
            q = q.Where(s => EF.Functions.ILike(s.Name, $"%{term}%"));
        }

        // Sorting
        q = request.Sort?.Trim() switch
        {
            "name"        => q.OrderBy(s => s.Name).ThenBy(s => s.SeasonYear),
            "-name"       => q.OrderByDescending(s => s.Name).ThenByDescending(s => s.SeasonYear),
            "seasonYear"  => q.OrderBy(s => s.SeasonYear).ThenBy(s => s.Name),
            "-seasonYear" => q.OrderByDescending(s => s.SeasonYear).ThenByDescending(s => s.Name),
            _             => q.OrderBy(s => s.SeasonYear).ThenBy(s => s.Name)
        };

        // Project then paginate
        return await q.ToPaginatedResultAsync<Domain.Entities.Fixtures.Series, SeriesDto>(request.Page, request.PageSize, _mapper.ConfigurationProvider, ct: ct);
    }
}
