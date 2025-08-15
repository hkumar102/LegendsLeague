using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Contracts.Series;
using FixtureEntities = LegendsLeague.Domain.Entities.Fixtures;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Series.Commands.CreateSeries;

/// <summary>
/// Command to create a new cricket series/season (e.g., IPL 2026).
/// Enforces uniqueness on (Name, SeasonYear) in a case-insensitive manner.
/// </summary>
/// <param name="Name">Human-friendly series name (e.g., "Indian Premier League").</param>
/// <param name="SeasonYear">Year of the season (e.g., 2026).</param>
public sealed record CreateSeriesCommand(string Name, int SeasonYear) : IRequest<SeriesDto>;

/// <summary>
/// Handles <see cref="CreateSeriesCommand"/> by inserting a <see cref="Series"/> row,
/// after verifying there is no existing series with the same (Name, SeasonYear),
/// using a case-insensitive comparison for the name.
/// </summary>
public sealed class CreateSeriesCommandHandler : IRequestHandler<CreateSeriesCommand, SeriesDto>
{
    private readonly IFixturesDbContext _db;

    /// <summary>
    /// Initializes a new instance of the handler.
    /// </summary>
    /// <param name="db">Fixtures persistence abstraction.</param>
    public CreateSeriesCommandHandler(IFixturesDbContext db) => _db = db;

    /// <inheritdoc />
    public async Task<SeriesDto> Handle(CreateSeriesCommand request, CancellationToken ct)
    {
        var normalized = request.Name.Trim();

        // Duplicate check (provider-agnostic): case-insensitive name + same year
        var exists = await _db.Series
            .AsNoTracking()
            .AnyAsync(s => s.SeasonYear == request.SeasonYear &&
                           s.Name.ToLower() == normalized.ToLower(), ct);

        if (exists)
        {
            throw new ConflictException($"Series '{normalized}' ({request.SeasonYear}) already exists.");
        }

        var entity = new FixtureEntities.Series
        {
            Id = Guid.NewGuid(),
            Name = normalized,
            SeasonYear = request.SeasonYear
        };

        await _db.Series.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct); // auditing interceptor will populate Created*

        // Return DTO shape expected by clients
        return new SeriesDto(entity.Id, entity.Name, entity.SeasonYear);
    }
}
