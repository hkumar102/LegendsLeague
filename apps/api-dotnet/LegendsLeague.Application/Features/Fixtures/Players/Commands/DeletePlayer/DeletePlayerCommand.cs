
using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Players.Commands.DeletePlayer;

public sealed record DeletePlayerCommand(Guid Id) : IRequest;

public sealed class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand>
{
    private readonly IFixturesDbContext _db;

    public DeletePlayerCommandHandler(IFixturesDbContext db) => _db = db;

    public async Task Handle(DeletePlayerCommand request, CancellationToken ct)
    {
        var entity = await _db.Players.FirstOrDefaultAsync(p => p.Id == request.Id, ct);
        if (entity is null) throw new NotFoundException("Player not found.");

        _db.Players.Remove(entity); // soft-delete via interceptor
        await _db.SaveChangesAsync(ct);
    }
}
