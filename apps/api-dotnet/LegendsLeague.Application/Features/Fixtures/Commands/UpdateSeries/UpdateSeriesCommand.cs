using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Contracts.Series;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Commands.UpdateSeries;

/// <summary>
/// Command to update an existing series (e.g., rename or change season year).
/// Enforces uniqueness on (Name, SeasonYear) excluding the same entity.
/// </summary>
/// <param name="Id">Series identifier.</param>
/// <param name="Name">New name.</param>
/// <param name="SeasonYear">New year.</param>
public sealed record UpdateSeriesCommand(Guid Id, string Name, int SeasonYear) : IRequest<SeriesDto>;

/// <summary>
/// Handler for <see cref="UpdateSeriesCommand"/>.
/// </summary>
public sealed class UpdateSeriesCommandHandler : IRequestHandler<UpdateSeriesCommand, SeriesDto>
{
    private readonly IFixturesDbContext _db;

    public UpdateSeriesCommandHandler(IFixturesDbContext db) => _db = db;

    public async Task<SeriesDto> Handle(UpdateSeriesCommand request, CancellationToken ct)
    {
        var entity = await _db.Series.FirstOrDefaultAsync(s => s.Id == request.Id, ct);
        if (entity is null)
            throw new NotFoundException($"Series '{request.Id}' was not found.");

        var normalized = request.Name.Trim();

        // Duplicate check (case-insensitive name) excluding the same entity
        var duplicate = await _db.Series.AsNoTracking().AnyAsync(
            s => s.Id != request.Id
              && s.SeasonYear == request.SeasonYear
              && s.Name.ToLower() == normalized.ToLower(), ct);

        if (duplicate)
            throw new ConflictException($"Series '{normalized}' ({request.SeasonYear}) already exists.");

        entity.Name = normalized;
        entity.SeasonYear = request.SeasonYear;

        await _db.SaveChangesAsync(ct);
        return new SeriesDto(entity.Id, entity.Name, entity.SeasonYear);
    }
}
