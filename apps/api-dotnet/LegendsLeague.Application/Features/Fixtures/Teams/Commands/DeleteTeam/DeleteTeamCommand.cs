using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Commands.DeleteTeam;

/// <summary>
/// Command to delete a team. Because <see cref="Domain.Entities.Fixtures.RealTeam"/> is soft-deletable,
/// calling Remove will be intercepted and converted to a soft delete.
/// </summary>
/// <param name="Id">Team identifier.</param>
public sealed record DeleteTeamCommand(Guid Id) : IRequest<Unit>;

/// <summary>
/// Handles <see cref="DeleteTeamCommand"/> by removing (soft-deleting) the entity.
/// </summary>
public sealed class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, Unit>
{
    private readonly IFixturesDbContext _db;

    /// <summary>Initializes a new handler.</summary>
    public DeleteTeamCommandHandler(IFixturesDbContext db) => _db = db;

    /// <inheritdoc />
    public async Task<Unit> Handle(DeleteTeamCommand request, CancellationToken ct)
    {
        var entity = await _db.RealTeams.FirstOrDefaultAsync(t => t.Id == request.Id, ct);
        if (entity is null)
            throw new NotFoundException($"Team '{request.Id}' was not found.");

        _db.RealTeams.Remove(entity); // Interceptor flips to soft delete
        await _db.SaveChangesAsync(ct);

        return Unit.Value;
    }
}
