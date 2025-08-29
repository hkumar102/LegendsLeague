using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Application.Features.Fantasy.Drafts.Commands.MakePick;
using LegendsLeague.Application.Features.Fantasy.Drafts.Commands.UndoPick;
using LegendsLeague.Application.Features.Fantasy.Drafts.Queries.ListPicksByDraft;

namespace LegendsLeague.Api.Controllers.Fantasy;

/// <summary>Endpoints to manage picks in a fantasy draft.</summary>
[ApiController]
[Route("api/v1/drafts/{draftId:guid}/picks")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class DraftPicksController : ControllerBase
{
    private readonly ISender _sender;

    public DraftPicksController(ISender sender) => _sender = sender;

    /// <summary>List picks within a draft (paginated).</summary>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<DraftPickDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<DraftPickDto>>> List(
        [FromRoute] Guid draftId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] string? sort = null,
        CancellationToken ct = default)
    {
        var result = await _sender.Send(new ListPicksByDraftQuery(
            DraftId: draftId, Page: page, PageSize: pageSize, Sort: sort
        ), ct);

        return Ok(result);
    }

    /// <summary>Make a pick in a live draft.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(DraftPickDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DraftPickDto>> MakePick(
        [FromRoute] Guid draftId,
        [FromBody] MakePickRequest body,
        CancellationToken ct = default)
    {
        var dto = await _sender.Send(new MakePickCommand(
            DraftId: draftId,
            LeagueTeamId: body.LeagueTeamId,
            PlayerId: body.PlayerId,
            OverallPickNumber: body.OverallPickNumber
        ), ct);

        return CreatedAtAction(nameof(List), new { draftId, page = 1, pageSize = 50 }, dto);
    }

    /// <summary>Undo a previously completed pick.</summary>
    [HttpDelete("{overallPickNumber:int}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UndoPick(
        [FromRoute] Guid draftId,
        [FromRoute] int overallPickNumber,
        CancellationToken ct = default)
    {
        var ok = await _sender.Send(new UndoPickCommand(
            DraftId: draftId,
            OverallPickNumber: overallPickNumber
        ), ct);

        if (!ok) return NotFound();
        return NoContent();
    }
}
