using System.Net.Mime;
using LegendsLeague.Contracts.Players;
using LegendsLeague.Application.Features.Fixtures.Players.Queries;
using LegendsLeague.Application.Features.Fixtures.Players.Commands.UpdatePlayer;
using LegendsLeague.Application.Features.Fixtures.Players.Commands.DeletePlayer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// ✅ Enum aliases at file-scope (avoid conflicts with Contracts enums)
using DomainRole    = LegendsLeague.Domain.Entities.Fixtures.Enums.PlayerRole;
using DomainBatting = LegendsLeague.Domain.Entities.Fixtures.Enums.BattingStyle;
using DomainBowling = LegendsLeague.Domain.Entities.Fixtures.Enums.BowlingStyle;

namespace LegendsLeague.Api.Controllers.Fixtures;

/// <summary>
/// Player resource endpoints (by id: get/update/delete).
/// </summary>
[ApiController]
[Route("api/v1/players")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class PlayersController : ControllerBase
{
    private readonly ISender _sender;
    public PlayersController(ISender sender) => _sender = sender;

    /// <summary>Gets a player by id.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlayerDto>> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        var dto = await _sender.Send(new GetPlayerByIdQuery(id), ct);
        if (dto is null) return NotFound();
        return Ok(dto);
    }

    /// <summary>Updates a player.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlayerDto>> Update([FromRoute] Guid id, [FromBody] PlayerUpdateRequest body, CancellationToken ct)
    {
        var cmd = new UpdatePlayerCommand(
            Id: id,
            RealTeamId: body.RealTeamId,
            FullName: body.FullName,
            ShortName: body.ShortName,
            Country: body.Country,
            Role: body.Role.HasValue ? (DomainRole?) (int)body.Role.Value : null,
            Batting: body.Batting.HasValue ? (DomainBatting?) (int)body.Batting.Value : null,
            Bowling: body.Bowling.HasValue ? (DomainBowling?) (int)body.Bowling.Value : null
        );

        var dto = await _sender.Send(cmd, ct);
        return Ok(dto);
    }

    /// <summary>Deletes (soft) a player.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        await _sender.Send(new DeletePlayerCommand(id), ct);
        return NoContent();
    }
}
