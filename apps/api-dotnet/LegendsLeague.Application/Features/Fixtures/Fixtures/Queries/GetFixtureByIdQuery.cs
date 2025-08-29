using AutoMapper;
using AutoMapper.QueryableExtensions;

using LegendsLeague.Contracts.Fixtures;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Fixtures.Queries;

/// <summary>Query to fetch a single fixture by Id.</summary>
/// <param name="Id">Fixture identifier.</param>
public sealed record GetFixtureByIdQuery(Guid Id) : IRequest<FixtureDto?>;

/// <summary>Handler for <see cref="GetFixtureByIdQuery"/>.</summary>
public sealed class GetFixtureByIdQueryHandler : IRequestHandler<GetFixtureByIdQuery, FixtureDto?>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public GetFixtureByIdQueryHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<FixtureDto?> Handle(GetFixtureByIdQuery request, CancellationToken ct)
    {
        return await _db.Fixtures
            .AsNoTracking()
            .Where(f => f.Id == request.Id)
            .ProjectTo<FixtureDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);
    }
}
