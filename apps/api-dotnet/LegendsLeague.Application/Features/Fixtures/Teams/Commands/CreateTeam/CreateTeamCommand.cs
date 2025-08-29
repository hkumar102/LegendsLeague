using AutoMapper;

using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Contracts.Teams;
using LegendsLeague.Domain.Abstractions.Persistence;
using LegendsLeague.Domain.Entities.Fixtures;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Commands.CreateTeam;

/// <summary>
/// Command to create a real team within a specific series.
/// </summary>
public sealed record CreateTeamCommand(Guid SeriesId, string Name, string? ShortName) : IRequest<RealTeamDto>;

public sealed class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, RealTeamDto>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public CreateTeamCommandHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<RealTeamDto> Handle(CreateTeamCommand request, CancellationToken ct)
    {
        var seriesExists = await _db.Series.AsNoTracking().AnyAsync(s => s.Id == request.SeriesId, ct);
        if (!seriesExists) throw new NotFoundException($"Series '{request.SeriesId}' was not found.");

        var normalizedName = request.Name.Trim();

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

        return _mapper.Map<RealTeamDto>(entity);
    }
}
