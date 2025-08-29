using AutoMapper;
using AutoMapper.QueryableExtensions;

using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Players;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Players.Queries;

/// <summary>
/// Paged list of players filtered by series, with optional search on name/short/country.
/// </summary>
public sealed record GetPlayersBySeriesQuery(
    Guid SeriesId,
    string? Search = null,
    int Page = 1,
    int PageSize = 20
) : IRequest<PaginatedResult<PlayerDto>>;

/// <summary>Handler for <see cref="GetPlayersBySeriesQuery"/>.</summary>
public sealed class GetPlayersBySeriesQueryHandler : IRequestHandler<GetPlayersBySeriesQuery, PaginatedResult<PlayerDto>>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public GetPlayersBySeriesQueryHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<PlayerDto>> Handle(GetPlayersBySeriesQuery request, CancellationToken ct)
    {
        IQueryable<Domain.Entities.Fixtures.Player> q = _db.Players.AsNoTracking()
            .Where(p => p.SeriesId == request.SeriesId);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var s = request.Search.Trim();
            q = q.Where(p =>
                EF.Functions.ILike(p.FullName, $"%{s}%") ||
                (p.ShortName != null && EF.Functions.ILike(p.ShortName, $"%{s}%")) ||
                (p.Country != null && EF.Functions.ILike(p.Country, $"%{s}%"))
            );
        }

        q = q.OrderBy(p => p.FullName).ThenBy(p => p.Id);

        return await q.ToPaginatedResultAsync<Domain.Entities.Fixtures.Player, PlayerDto>(
            request.Page,
            request.PageSize,
            _mapper.ConfigurationProvider,
            ct: ct
        );
    }
}
