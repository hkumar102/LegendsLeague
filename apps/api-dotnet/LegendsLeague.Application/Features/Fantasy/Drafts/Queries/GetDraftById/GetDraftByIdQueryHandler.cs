using AutoMapper;
using AutoMapper.QueryableExtensions;
using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Queries.GetDraftById;

/// <summary>Handles retrieving a single draft as DTO.</summary>
public sealed class GetDraftByIdQueryHandler : IRequestHandler<GetDraftByIdQuery, DraftDto?>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public GetDraftByIdQueryHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<DraftDto?> Handle(GetDraftByIdQuery request, CancellationToken ct)
    {
        return await _db.Drafts.AsNoTracking()
            .Where(d => d.Id == request.DraftId)
            .ProjectTo<DraftDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);
    }
}
