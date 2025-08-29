using AutoMapper;
using LegendsLeague.Application.Common.Extensions;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Queries.ListPicksByDraft;

/// <summary>Handles paginated listing of draft picks.</summary>
public sealed class ListPicksByDraftQueryHandler
    : IRequestHandler<ListPicksByDraftQuery, PaginatedResult<DraftPickDto>>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public ListPicksByDraftQueryHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<DraftPickDto>> Handle(ListPicksByDraftQuery request, CancellationToken ct)
    {
        var q = _db.DraftPicks.AsNoTracking().Where(p => p.DraftId == request.DraftId);

        q = request.Sort?.Trim() switch
        {
            "-pick" => q.OrderByDescending(p => p.OverallPickNumber),
            "pick"  => q.OrderBy(p => p.OverallPickNumber),
            _       => q.OrderBy(p => p.OverallPickNumber)
        };

        return await q.ToPaginatedResultAsync<LegendsLeague.Domain.Entities.Fantasy.DraftPick, DraftPickDto>(
            request.Page, request.PageSize,_mapper.ConfigurationProvider, ct: ct);
    }
}
