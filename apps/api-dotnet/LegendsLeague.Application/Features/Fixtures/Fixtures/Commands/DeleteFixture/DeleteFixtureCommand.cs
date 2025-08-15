using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Fixtures.Commands.DeleteFixture;

/// <summary>Deletes (soft) a fixture by Id.</summary>
public sealed record DeleteFixtureCommand(Guid Id) : IRequest;

public sealed class DeleteFixtureCommandHandler : IRequestHandler<DeleteFixtureCommand>
{
    private readonly IFixturesDbContext _db;

    public DeleteFixtureCommandHandler(IFixturesDbContext db) => _db = db;

    public async Task Handle(DeleteFixtureCommand request, CancellationToken ct)
    {
        var entity = await _db.Fixtures.FirstOrDefaultAsync(f => f.Id == request.Id, ct);
        if (entity is null) throw new NotFoundException("Fixture not found.");

        _db.Fixtures.Remove(entity); // soft-delete via interceptor
        await _db.SaveChangesAsync(ct);
    }
}
