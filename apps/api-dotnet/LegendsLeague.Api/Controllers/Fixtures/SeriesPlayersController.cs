using System.Net.Mime;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Players;
using LegendsLeague.Application.Features.Fixtures.Players.Queries;
using LegendsLeague.Application.Features.Fixtures.Players.Commands.CreatePlayer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// ✅ Enum aliases at file-scope (avoid conflicts with Contracts enums)
using DomainRole    = LegendsLeague.Domain.Entities.Fixtures.Enums.PlayerRole;
using DomainBatting = LegendsLeague.Domain.Entities.Fixtures.Enums.BattingStyle;
using DomainBowling = LegendsLeague.Domain.Entities.Fixtures.Enums.BowlingStyle;

namespace LegendsLeague.Api.Controllers.Fixtures;

/// <summary>
/// Series-scoped players endpoints (list/search and create).
/// Implemented with absolute route templates to avoid editing SeriesController.
/// </summary>
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public sealed class SeriesPlayersController : ControllerBase
{
    private readonly ISender _sender;
    public SeriesPlayersController(ISender sender) => _sender = sender;

    /// <summary>Lists players in a series (paged, optional search).</summary>
    [HttpGet("/api/v1/series/{seriesId:guid}/players")]
    [ProducesResponseType(typeof(PaginatedResult<PlayerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<PlayerDto>>> GetBySeries(
        [FromRoute] Guid seriesId,
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        var result = await _sender.Send(new GetPlayersBySeriesQuery(
            SeriesId: seriesId,
            Search: search,
            Page: page,
            PageSize: pageSize
        ), ct);

        return Ok(result);
    }

    /// <summary>Lists players in a specific team of a series (paged, optional search).</summary>
    [HttpGet("/api/v1/series/{seriesId:guid}/teams/{teamId:guid}/players")]
    [ProducesResponseType(typeof(PaginatedResult<PlayerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<PlayerDto>>> GetByTeam(
        [FromRoute] Guid seriesId,
        [FromRoute] Guid teamId,
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        var result = await _sender.Send(new GetPlayersByTeamQuery(
            SeriesId: seriesId,
            RealTeamId: teamId,
            Search: search,
            Page: page,
            PageSize: pageSize
        ), ct);

        return Ok(result);
    }

    /// <summary>Creates a new player within a series and assigns to a team.</summary>
    [HttpPost("/api/v1/series/{seriesId:guid}/players")]
    [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlayerDto>> Create(
        [FromRoute] Guid seriesId,
        [FromBody] PlayerCreateRequest body,
        CancellationToken ct)
    {
        var cmd = new CreatePlayerCommand(
            SeriesId: seriesId,
            RealTeamId: body.RealTeamId,
            FullName: body.FullName,
            ShortName: body.ShortName,
            Country: body.Country,
            Role: (DomainRole)(int)body.Role,
            Batting: (DomainBatting)(int)body.Batting,
            Bowling: (DomainBowling)(int)body.Bowling
        );

        var created = await _sender.Send(cmd, ct);
        return CreatedAtAction(
            nameof(PlayersController.GetById),
            "Players",
            new { id = created.Id },
            created
        );
    }
}
