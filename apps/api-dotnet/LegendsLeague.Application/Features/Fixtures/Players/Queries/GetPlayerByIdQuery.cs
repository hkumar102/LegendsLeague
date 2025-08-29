using AutoMapper;
using AutoMapper.QueryableExtensions;

using LegendsLeague.Contracts.Players;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Players.Queries;

/// <summary>Query to fetch a single player by Id.</summary>
/// <param name="Id">Player identifier.</param>
public sealed record GetPlayerByIdQuery(Guid Id) : IRequest<PlayerDto?>;

/// <summary>Handler for <see cref="GetPlayerByIdQuery"/>.</summary>
public sealed class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, PlayerDto?>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public GetPlayerByIdQueryHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PlayerDto?> Handle(GetPlayerByIdQuery request, CancellationToken ct)
    {
        return await _db.Players
            .AsNoTracking()
            .Where(p => p.Id == request.Id)
            .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);
    }
}
