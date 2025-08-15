using AutoMapper;
using AutoMapper.QueryableExtensions;
using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Players;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Players.Queries;

/// <summary>
/// Global search for players across series (primarily for admin tools).
/// </summary>
public sealed record SearchPlayersQuery(
    string? Search = null,
    int Page = 1,
    int PageSize = 20
) : IRequest<PaginatedResult<PlayerDto>>;

/// <summary>Handler for <see cref="SearchPlayersQuery"/>.</summary>
public sealed class SearchPlayersQueryHandler : IRequestHandler<SearchPlayersQuery, PaginatedResult<PlayerDto>>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public SearchPlayersQueryHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<PlayerDto>> Handle(SearchPlayersQuery request, CancellationToken ct)
    {
        IQueryable<LegendsLeague.Domain.Entities.Fixtures.Player> q = _db.Players.AsNoTracking();

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

        return await q
            .ToPaginatedResultAsync<Domain.Entities.Fixtures.Player, PlayerDto>(request.Page, request.PageSize,
                _mapper.ConfigurationProvider, ct: ct);
    }
}
