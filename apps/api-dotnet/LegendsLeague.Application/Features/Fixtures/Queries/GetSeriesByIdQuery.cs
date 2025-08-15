using AutoMapper;
using AutoMapper.QueryableExtensions;
using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Contracts.Series;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Queries;

/// <summary>Query to retrieve a single series by its identifier.</summary>
public sealed record GetSeriesByIdQuery(Guid Id) : IRequest<SeriesDto?>;

/// <summary>Handler using AutoMapper projection.</summary>
public sealed class GetSeriesByIdQueryHandler : IRequestHandler<GetSeriesByIdQuery, SeriesDto?>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public GetSeriesByIdQueryHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<SeriesDto?> Handle(GetSeriesByIdQuery request, CancellationToken ct)
    {
        return await _db.Series
            .AsNoTracking()
            .Where(s => s.Id == request.Id)
            .ProjectTo<SeriesDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);
    }
}
