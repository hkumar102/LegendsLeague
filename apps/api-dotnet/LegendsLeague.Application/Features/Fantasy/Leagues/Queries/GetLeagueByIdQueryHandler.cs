using AutoMapper;
using AutoMapper.QueryableExtensions;

using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Queries;

/// <summary>
/// Handles <see cref="GetLeagueByIdQuery"/> and returns <c>null</c> when not found.
/// </summary>
public sealed class GetLeagueByIdQueryHandler : IRequestHandler<GetLeagueByIdQuery, FantasyLeagueDto?>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public GetLeagueByIdQueryHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<FantasyLeagueDto?> Handle(GetLeagueByIdQuery request, CancellationToken ct)
    {
        return await _db.Leagues
            .AsNoTracking()
            .Where(l => l.Id == request.Id)
            .ProjectTo<FantasyLeagueDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);
    }
}
