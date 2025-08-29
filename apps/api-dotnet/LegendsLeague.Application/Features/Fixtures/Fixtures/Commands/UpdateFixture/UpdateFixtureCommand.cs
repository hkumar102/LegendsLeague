using System.ComponentModel.DataAnnotations;
using AutoMapper;

using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Contracts.Fixtures;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Fixtures.Commands.UpdateFixture;

/// <summary>Updates an existing fixture.</summary>
public sealed record UpdateFixtureCommand(
    Guid Id,
    Guid? HomeTeamId = null,
    Guid? AwayTeamId = null,
    DateTimeOffset? StartTimeUtc = null
) : IRequest<FixtureDto>;

public sealed class UpdateFixtureCommandHandler : IRequestHandler<UpdateFixtureCommand, FixtureDto>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public UpdateFixtureCommandHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<FixtureDto> Handle(UpdateFixtureCommand request, CancellationToken ct)
    {
        var entity = await _db.Fixtures.FirstOrDefaultAsync(f => f.Id == request.Id, ct);
        if (entity is null) throw new NotFoundException("Fixture not found.");

        var newHome = request.HomeTeamId ?? entity.HomeTeamId;
        var newAway = request.AwayTeamId ?? entity.AwayTeamId;
        if (newHome == newAway)
            throw new ValidationException("HomeTeamId must be different from AwayTeamId.");

        // If team ids changed, ensure both belong to the same series as the fixture.
        if (request.HomeTeamId.HasValue || request.AwayTeamId.HasValue)
        {
            var teamIds = new[] { newHome, newAway };
            var count = await _db.RealTeams.CountAsync(t => t.SeriesId == entity.SeriesId && teamIds.Contains(t.Id), ct);
            if (count != 2)
                throw new NotFoundException("One or both teams were not found in the fixture's series.");
        }

        entity.HomeTeamId = newHome;
        entity.AwayTeamId = newAway;
        if (request.StartTimeUtc.HasValue) entity.StartTimeUtc = request.StartTimeUtc.Value;

        await _db.SaveChangesAsync(ct);
        return _mapper.Map<FixtureDto>(entity);
    }
}
