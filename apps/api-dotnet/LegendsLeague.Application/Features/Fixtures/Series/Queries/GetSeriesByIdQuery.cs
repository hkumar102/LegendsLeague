
using LegendsLeague.Contracts.Series;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Series.Queries;

/// <summary>Query to retrieve a single series by its identifier.</summary>
/// <param name="Id">Series identifier.</param>
public sealed record GetSeriesByIdQuery(Guid Id) : IRequest<SeriesDto?>;

public sealed class GetSeriesByIdQueryHandler : IRequestHandler<GetSeriesByIdQuery, SeriesDto?>
{
    private readonly IFixturesDbContext _db;

    public GetSeriesByIdQueryHandler(IFixturesDbContext db) => _db = db;

    public async Task<SeriesDto?> Handle(GetSeriesByIdQuery request, CancellationToken ct)
    {
        return await _db.Series.AsNoTracking()
            .Where(s => s.Id == request.Id)
            .Select(s => new SeriesDto(s.Id, s.Name, s.SeasonYear))
            .FirstOrDefaultAsync(ct);
    }
}
