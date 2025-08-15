using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Contracts.Teams;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Queries;

/// <summary>
/// Query to fetch a single team by its identifier.
/// Returns <c>null</c> if not found (controller can translate to 404).
/// </summary>
/// <param name="Id">Team identifier.</param>
public sealed record GetTeamByIdQuery(Guid Id) : IRequest<RealTeamDto?>;

/// <summary>
/// Handles <see cref="GetTeamByIdQuery"/> using the fixtures read surface.
/// </summary>
public sealed class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, RealTeamDto?>
{
    private readonly IFixturesDbContext _db;

    /// <summary>Initializes handler.</summary>
    public GetTeamByIdQueryHandler(IFixturesDbContext db) => _db = db;

    /// <inheritdoc />
    public async Task<RealTeamDto?> Handle(GetTeamByIdQuery request, CancellationToken ct)
    {
        var dto = await _db.RealTeams
            .AsNoTracking()
            .Where(t => t.Id == request.Id)
            .Select(t => new RealTeamDto(t.Id, t.SeriesId, t.Name, t.ShortName))
            .FirstOrDefaultAsync(ct);

        return dto; // may be null
    }
}
