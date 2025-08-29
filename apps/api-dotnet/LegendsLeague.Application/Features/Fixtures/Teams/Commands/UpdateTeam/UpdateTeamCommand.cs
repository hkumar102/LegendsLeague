using AutoMapper;

using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Contracts.Teams;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Commands.UpdateTeam;

/// <summary>Update a team's name/short name.</summary>
public sealed record UpdateTeamCommand(Guid Id, string Name, string? ShortName) : IRequest<RealTeamDto>;

public sealed class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, RealTeamDto>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public UpdateTeamCommandHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<RealTeamDto> Handle(UpdateTeamCommand request, CancellationToken ct)
    {
        var entity = await _db.RealTeams.FirstOrDefaultAsync(t => t.Id == request.Id, ct);
        if (entity is null) throw new NotFoundException($"Team '{request.Id}' was not found.");

        var normalizedName = request.Name.Trim();

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

        return _mapper.Map<RealTeamDto>(entity);
    }
}
