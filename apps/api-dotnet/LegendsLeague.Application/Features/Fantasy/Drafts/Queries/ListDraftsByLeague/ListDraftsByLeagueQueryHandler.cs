using AutoMapper;
using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Queries.ListDraftsByLeague;

/// <summary>Handles paginated listing of drafts in a league.</summary>
public sealed class ListDraftsByLeagueQueryHandler
    : IRequestHandler<ListDraftsByLeagueQuery, PaginatedResult<DraftDto>>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public ListDraftsByLeagueQueryHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<DraftDto>> Handle(ListDraftsByLeagueQuery request, CancellationToken ct)
    {
        var q = _db.Drafts.AsNoTracking().Where(d => d.LeagueId == request.LeagueId);

        q = request.Sort?.Trim() switch
        {
            "-scheduled" => q.OrderByDescending(d => d.ScheduledAtUtc),
            "scheduled"  => q.OrderBy(d => d.ScheduledAtUtc),
            "-status"    => q.OrderByDescending(d => d.Status),
            "status"     => q.OrderBy(d => d.Status),
            _            => q.OrderByDescending(d => d.CreatedAtUtc)
        };

        return await q.ToPaginatedResultAsync<LegendsLeague.Domain.Entities.Fantasy.Draft, DraftDto>(
            request.Page, request.PageSize,_mapper.ConfigurationProvider, ct: ct);
    }
}
