using AutoMapper;
using AutoMapper.QueryableExtensions;

using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fixtures;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Fixtures.Queries;

/// <summary>
/// Global search for fixtures with optional team name filter and date range.
/// </summary>
public sealed record SearchFixturesQuery(
    string? TeamName = null,
    DateTimeOffset? FromUtc = null,
    DateTimeOffset? ToUtc = null,
    int Page = 1,
    int PageSize = 20
) : IRequest<PaginatedResult<FixtureDto>>;

public sealed class SearchFixturesQueryHandler : IRequestHandler<SearchFixturesQuery, PaginatedResult<FixtureDto>>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public SearchFixturesQueryHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<FixtureDto>> Handle(SearchFixturesQuery request, CancellationToken ct)
    {
        // Base
        var fixtures = _db.Fixtures.AsNoTracking().AsQueryable();

        // Date filters
        if (request.FromUtc.HasValue) fixtures = fixtures.Where(f => f.StartTimeUtc >= request.FromUtc.Value);
        if (request.ToUtc.HasValue)   fixtures = fixtures.Where(f => f.StartTimeUtc <= request.ToUtc.Value);

        // Optional team name filter: join to teams and apply ILIKE on name/short name
        if (!string.IsNullOrWhiteSpace(request.TeamName))
        {
            var tn = request.TeamName.Trim();
            fixtures =
                from f in fixtures
                join ht in _db.RealTeams.AsNoTracking() on f.HomeTeamId equals ht.Id
                join at in _db.RealTeams.AsNoTracking() on f.AwayTeamId equals at.Id
                where EF.Functions.ILike(ht.Name, $"%{tn}%")
                   || EF.Functions.ILike(at.Name, $"%{tn}%")
                   || (ht.ShortName != null && EF.Functions.ILike(ht.ShortName, $"%{tn}%"))
                   || (at.ShortName != null && EF.Functions.ILike(at.ShortName, $"%{tn}%"))
                select f;
        }

        fixtures = fixtures.OrderBy(f => f.StartTimeUtc).ThenBy(f => f.Id);

        return await fixtures
                             .ToPaginatedResultAsync<Domain.Entities.Fixtures.Fixture, FixtureDto>(request.Page, request.PageSize, _mapper.ConfigurationProvider);
    }
}
