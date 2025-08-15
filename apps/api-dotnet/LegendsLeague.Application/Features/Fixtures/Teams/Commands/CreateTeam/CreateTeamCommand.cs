using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Contracts.Teams;
using LegendsLeague.Domain.Entities.Fixtures;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Commands.CreateTeam;

/// <summary>
/// Command to create a real team within a specific series.
/// Enforces unique team name per series (case-insensitive).
/// </summary>
/// <param name="SeriesId">The owning series identifier.</param>
/// <param name="Name">Team name (unique per series; case-insensitive).</param>
/// <param name="ShortName">Optional short code (<= 10 chars).</param>
public sealed record CreateTeamCommand(Guid SeriesId, string Name, string? ShortName) : IRequest<RealTeamDto>;

/// <summary>
/// Handles <see cref="CreateTeamCommand"/> by inserting a <see cref="RealTeam"/> after validations.
/// </summary>
public sealed class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, RealTeamDto>
{
    private readonly IFixturesDbContext _db;

    /// <summary>Initializes a new handler.</summary>
    public CreateTeamCommandHandler(IFixturesDbContext db) => _db = db;

    /// <inheritdoc />
    public async Task<RealTeamDto> Handle(CreateTeamCommand request, CancellationToken ct)
    {
        // Ensure series exists
        var seriesExists = await _db.Series.AsNoTracking().AnyAsync(s => s.Id == request.SeriesId, ct);
        if (!seriesExists)
            throw new NotFoundException($"Series '{request.SeriesId}' was not found.");

        var normalizedName = request.Name.Trim();

        // Uniqueness within series (ignore soft-deleted rows)
        var duplicate = await _db.RealTeams.AsNoTracking().AnyAsync(
            t => t.SeriesId == request.SeriesId
              && !t.IsDeleted
              && t.Name.ToLower() == normalizedName.ToLower(), ct);

        if (duplicate)
            throw new ConflictException($"Team '{normalizedName}' already exists in this series.");

        var entity = new RealTeam
        {
            Id        = Guid.NewGuid(),
            SeriesId  = request.SeriesId,
            Name      = normalizedName,
            ShortName = string.IsNullOrWhiteSpace(request.ShortName) ? null : request.ShortName.Trim()
        };

        await _db.RealTeams.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);

        return new RealTeamDto(entity.Id, entity.SeriesId, entity.Name, entity.ShortName);
    }
}
