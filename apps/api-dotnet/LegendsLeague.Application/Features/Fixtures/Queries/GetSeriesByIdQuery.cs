using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Contracts.Series;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Queries;

/// <summary>
/// Query to retrieve a single series by its identifier.
/// </summary>
/// <param name="Id">Series identifier.</param>
public sealed record GetSeriesByIdQuery(Guid Id) : IRequest<SeriesDto?>;

/// <summary>
/// Handles <see cref="GetSeriesByIdQuery"/> and returns <c>null</c> if not found.
/// </summary>
public sealed class GetSeriesByIdQueryHandler : IRequestHandler<GetSeriesByIdQuery, SeriesDto?>
{
    private readonly IFixturesDbContext _db;

    /// <summary>
    /// Initializes a new instance of the handler.
    /// </summary>
    /// <param name="db">Fixtures read/write abstraction.</param>
    public GetSeriesByIdQueryHandler(IFixturesDbContext db) => _db = db;

    /// <inheritdoc />
    public async Task<SeriesDto?> Handle(GetSeriesByIdQuery request, CancellationToken ct)
    {
        var dto = await _db.Series
            .AsNoTracking()
            .Where(s => s.Id == request.Id)
            .Select(s => new SeriesDto(s.Id, s.Name, s.SeasonYear))
            .FirstOrDefaultAsync(ct);

        return dto;
    }
}
