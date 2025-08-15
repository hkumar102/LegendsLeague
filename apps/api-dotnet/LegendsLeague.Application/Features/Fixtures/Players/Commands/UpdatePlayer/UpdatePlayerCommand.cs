using AutoMapper;
using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Contracts.Players; // for PlayerDto only
using MediatR;
using Microsoft.EntityFrameworkCore;

// Domain enum aliases (avoid ambiguity with Contracts enums)
using DomainRole = LegendsLeague.Domain.Entities.Fixtures.Enums.PlayerRole;
using DomainBatting = LegendsLeague.Domain.Entities.Fixtures.Enums.BattingStyle;
using DomainBowling = LegendsLeague.Domain.Entities.Fixtures.Enums.BowlingStyle;

namespace LegendsLeague.Application.Features.Fixtures.Players.Commands.UpdatePlayer;

/// <summary>
/// Updates a player's core profile and/or team assignment.
/// </summary>
public sealed record UpdatePlayerCommand(
    Guid Id,
    Guid? RealTeamId = null,
    string? FullName = null,
    string? ShortName = null,
    string? Country = null,
    DomainRole? Role = null,
    DomainBatting? Batting = null,
    DomainBowling? Bowling = null
) : IRequest<PlayerDto>;

public sealed class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand, PlayerDto>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public UpdatePlayerCommandHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PlayerDto> Handle(UpdatePlayerCommand request, CancellationToken ct)
    {
        var entity = await _db.Players.FirstOrDefaultAsync(p => p.Id == request.Id, ct);
        if (entity is null) throw new NotFoundException("Player not found.");

        // If moving teams, validate team is in the same series
        if (request.RealTeamId.HasValue && request.RealTeamId.Value != entity.RealTeamId)
        {
            var team = await _db.RealTeams
                .FirstOrDefaultAsync(t => t.Id == request.RealTeamId.Value && t.SeriesId == entity.SeriesId, ct);
            if (team is null) throw new NotFoundException("Target team not found in the player's series.");
            entity.RealTeamId = request.RealTeamId.Value;
        }

        if (!string.IsNullOrWhiteSpace(request.FullName)) entity.FullName = request.FullName!;
        if (request.ShortName is not null) entity.ShortName = request.ShortName;
        if (request.Country is not null) entity.Country = request.Country;
        if (request.Role.HasValue) entity.Role = request.Role.Value;
        if (request.Batting.HasValue) entity.Batting = request.Batting.Value;
        if (request.Bowling.HasValue) entity.Bowling = request.Bowling.Value;

        await _db.SaveChangesAsync(ct);
        return _mapper.Map<PlayerDto>(entity);
    }
}
