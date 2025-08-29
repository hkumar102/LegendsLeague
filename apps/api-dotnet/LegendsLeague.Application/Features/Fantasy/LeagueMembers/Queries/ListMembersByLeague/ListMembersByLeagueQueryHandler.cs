using AutoMapper;

using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Queries.ListMembersByLeague;

/// <summary>Handles ListMembersByLeagueQuery (paginated).</summary>
public sealed class ListMembersByLeagueQueryHandler
    : IRequestHandler<ListMembersByLeagueQuery, PaginatedResult<LeagueMemberDto>>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public ListMembersByLeagueQueryHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<LeagueMemberDto>> Handle(ListMembersByLeagueQuery request, CancellationToken ct)
    {
        var q = _db.LeagueMembers.AsNoTracking().Where(m => m.LeagueId == request.LeagueId);

        q = request.Sort?.Trim() switch
        {
            "-joined" => q.OrderByDescending(m => m.JoinedAtUtc),
            "joined" => q.OrderBy(m => m.JoinedAtUtc),
            _ => q.OrderBy(m => m.UserId)
        };

        return await q.ToPaginatedResultAsync<Domain.Entities.Fantasy.LeagueMember, LeagueMemberDto>(
            request.Page, request.PageSize, _mapper.ConfigurationProvider, ct: ct);
    }
}
