using System.ComponentModel.DataAnnotations;
using AutoMapper;

using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Contracts.Fixtures;
using LegendsLeague.Domain.Abstractions.Persistence;
using LegendsLeague.Domain.Entities.Fixtures;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Fixtures.Commands.CreateFixture;

/// <summary>Creates a new fixture in a series.</summary>
public sealed record CreateFixtureCommand(
    Guid SeriesId,
    Guid HomeTeamId,
    Guid AwayTeamId,
    DateTimeOffset StartTimeUtc
) : IRequest<FixtureDto>;

public sealed class CreateFixtureCommandHandler : IRequestHandler<CreateFixtureCommand, FixtureDto>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public CreateFixtureCommandHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<FixtureDto> Handle(CreateFixtureCommand request, CancellationToken ct)
    {
        if (request.HomeTeamId == request.AwayTeamId)
            throw new ValidationException("HomeTeamId must be different from AwayTeamId.");

        // Validate series
        var seriesExists = await _db.Series.AnyAsync(s => s.Id == request.SeriesId, ct);
        if (!seriesExists) throw new NotFoundException("Series not found.");

        // Validate teams exist and belong to series
        var teams = await _db.RealTeams
            .Where(t => t.SeriesId == request.SeriesId && (t.Id == request.HomeTeamId || t.Id == request.AwayTeamId))
            .Select(t => t.Id)
            .ToListAsync(ct);

        if (teams.Count != 2)
            throw new NotFoundException("One or both teams were not found in the specified series.");

        // Optional: prevent exact duplicate at same time for same pairing
        var dup = await _db.Fixtures.AnyAsync(f =>
            f.SeriesId == request.SeriesId &&
            ((f.HomeTeamId == request.HomeTeamId && f.AwayTeamId == request.AwayTeamId) ||
             (f.HomeTeamId == request.AwayTeamId && f.AwayTeamId == request.HomeTeamId)) &&
            f.StartTimeUtc == request.StartTimeUtc, ct);

        if (dup) throw new ConflictException("A fixture with the same teams and start time already exists.");

        var entity = new Fixture
        {
            Id = Guid.NewGuid(),
            SeriesId = request.SeriesId,
            HomeTeamId = request.HomeTeamId,
            AwayTeamId = request.AwayTeamId,
            StartTimeUtc = request.StartTimeUtc
        };

        await _db.Fixtures.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);

        return _mapper.Map<FixtureDto>(entity);
    }
}
