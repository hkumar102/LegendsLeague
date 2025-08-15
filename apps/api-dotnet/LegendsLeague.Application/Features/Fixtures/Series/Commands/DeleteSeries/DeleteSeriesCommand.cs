using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Series.Commands.DeleteSeries;

/// <summary>
/// Command to delete a series.
/// By default, refuses to delete if there are dependent real teams or fixtures unless <see cref="Force"/> is true.
/// </summary>
/// <param name="Id">Series identifier.</param>
/// <param name="Force">When true, deletes even if there are dependent rows.</param>
public sealed record DeleteSeriesCommand(Guid Id, bool Force = false) : IRequest<Unit>;

/// <summary>
/// Handler for <see cref="DeleteSeriesCommand"/>.
/// </summary>
public sealed class DeleteSeriesCommandHandler : IRequestHandler<DeleteSeriesCommand, Unit>
{
    private readonly IFixturesDbContext _db;

    public DeleteSeriesCommandHandler(IFixturesDbContext db) => _db = db;

    public async Task<Unit> Handle(DeleteSeriesCommand request, CancellationToken ct)
    {
        var entity = await _db.Series.FirstOrDefaultAsync(s => s.Id == request.Id, ct);
        if (entity is null)
            throw new NotFoundException($"Series '{request.Id}' was not found.");

        if (!request.Force)
        {
            var hasTeams = await _db.RealTeams.AsNoTracking().AnyAsync(t => t.SeriesId == request.Id, ct);
            var hasFixtures = await _db.Fixtures.AsNoTracking().AnyAsync(f => f.SeriesId == request.Id, ct);
            if (hasTeams || hasFixtures)
                throw new ConflictException("Series has dependent teams/fixtures. Use Force=true to delete.");
        }

        _db.Series.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
