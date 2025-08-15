using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Contracts.Teams;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Commands.UpdateTeam;

/// <summary>
/// Command to update a team's name/short name; keeps name unique within the same series.
/// </summary>
/// <param name="Id">Team identifier.</param>
/// <param name="Name">New team name.</param>
/// <param name="ShortName">New short code (optional).</param>
public sealed record UpdateTeamCommand(Guid Id, string Name, string? ShortName) : IRequest<RealTeamDto>;

/// <summary>
/// Handles <see cref="UpdateTeamCommand"/> against <see cref="IFixturesDbContext"/>.
/// </summary>
public sealed class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, RealTeamDto>
{
    private readonly IFixturesDbContext _db;

    /// <summary>Initializes a new handler.</summary>
    public UpdateTeamCommandHandler(IFixturesDbContext db) => _db = db;

    /// <inheritdoc />
    public async Task<RealTeamDto> Handle(UpdateTeamCommand request, CancellationToken ct)
    {
        var entity = await _db.RealTeams.FirstOrDefaultAsync(t => t.Id == request.Id, ct);
        if (entity is null)
            throw new NotFoundException($"Team '{request.Id}' was not found.");

        var normalizedName = request.Name.Trim();

        // Duplicate name within same series (excluding self, ignore soft-deleted)
        var duplicate = await _db.RealTeams.AsNoTracking().AnyAsync(
            t => t.Id != request.Id
              && t.SeriesId == entity.SeriesId
              && !t.IsDeleted
              && t.Name.ToLower() == normalizedName.ToLower(), ct);

        if (duplicate)
            throw new ConflictException($"Team '{normalizedName}' already exists in this series.");

        entity.Name      = normalizedName;
        entity.ShortName = string.IsNullOrWhiteSpace(request.ShortName) ? null : request.ShortName.Trim();

        await _db.SaveChangesAsync(ct);

        return new RealTeamDto(entity.Id, entity.SeriesId, entity.Name, entity.ShortName);
    }
}
