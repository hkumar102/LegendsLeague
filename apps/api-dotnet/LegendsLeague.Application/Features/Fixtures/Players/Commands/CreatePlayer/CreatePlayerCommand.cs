using AutoMapper;

using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Contracts.Players;
using LegendsLeague.Domain.Abstractions.Persistence; // for PlayerDto only
using LegendsLeague.Domain.Entities.Fixtures;
using MediatR;
using Microsoft.EntityFrameworkCore;

// Domain enum aliases (avoid ambiguity with Contracts enums)
using DomainRole = LegendsLeague.Domain.Entities.Fixtures.Enums.PlayerRole;
using DomainBatting = LegendsLeague.Domain.Entities.Fixtures.Enums.BattingStyle;
using DomainBowling = LegendsLeague.Domain.Entities.Fixtures.Enums.BowlingStyle;

namespace LegendsLeague.Application.Features.Fixtures.Players.Commands.CreatePlayer;

/// <summary>
/// Creates a new player in a series and assigns them to a real team.
/// </summary>
public sealed record CreatePlayerCommand(
    Guid SeriesId,
    Guid RealTeamId,
    string FullName,
    string? ShortName,
    string? Country,
    DomainRole Role,
    DomainBatting Batting,
    DomainBowling Bowling
) : IRequest<PlayerDto>;

public sealed class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, PlayerDto>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public CreatePlayerCommandHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PlayerDto> Handle(CreatePlayerCommand request, CancellationToken ct)
    {
        // Verify series exists
        var seriesExists = await _db.Series.AnyAsync(s => s.Id == request.SeriesId, ct);
        if (!seriesExists) throw new NotFoundException("Series not found.");

        // Verify team belongs to that series
        var team = await _db.RealTeams
            .FirstOrDefaultAsync(t => t.Id == request.RealTeamId && t.SeriesId == request.SeriesId, ct);
        if (team is null) throw new NotFoundException("Team not found in the specified series.");

        // Optional duplicate guard: same FullName within same team/series
        var dup = await _db.Players.AnyAsync(p =>
            p.SeriesId == request.SeriesId &&
            p.RealTeamId == request.RealTeamId &&
            p.FullName == request.FullName, ct);
        if (dup) throw new ConflictException("A player with the same name already exists in this team.");

        var entity = new Player
        {
            Id = Guid.NewGuid(),
            SeriesId = request.SeriesId,
            RealTeamId = request.RealTeamId,
            FullName = request.FullName,
            ShortName = request.ShortName,
            Country = request.Country,
            Role = request.Role,
            Batting = request.Batting,
            Bowling = request.Bowling
        };

        await _db.Players.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);

        return _mapper.Map<PlayerDto>(entity);
    }
}
