using AutoMapper;
using AutoMapper.QueryableExtensions;
using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fixtures;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Fixtures.Queries;

/// <summary>
/// Paged list of fixtures in a series (optional date range).
/// </summary>
public sealed record GetFixturesBySeriesQuery(
    Guid SeriesId,
    DateTimeOffset? FromUtc = null,
    DateTimeOffset? ToUtc = null,
    int Page = 1,
    int PageSize = 20
) : IRequest<PaginatedResult<FixtureDto>>;

public sealed class GetFixturesBySeriesQueryHandler : IRequestHandler<GetFixturesBySeriesQuery, PaginatedResult<FixtureDto>>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public GetFixturesBySeriesQueryHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<FixtureDto>> Handle(GetFixturesBySeriesQuery request, CancellationToken ct)
    {
        IQueryable<LegendsLeague.Domain.Entities.Fixtures.Fixture> q = _db.Fixtures.AsNoTracking()
            .Where(f => f.SeriesId == request.SeriesId);

        if (request.FromUtc.HasValue) q = q.Where(f => f.StartTimeUtc >= request.FromUtc.Value);
        if (request.ToUtc.HasValue)   q = q.Where(f => f.StartTimeUtc <= request.ToUtc.Value);

        q = q.OrderBy(f => f.StartTimeUtc).ThenBy(f => f.Id);

        return await q
                      .ToPaginatedResultAsync<LegendsLeague.Domain.Entities.Fixtures.Fixture, FixtureDto>(request.Page, request.PageSize, 
                          _mapper.ConfigurationProvider, ct: ct);
    }
}
