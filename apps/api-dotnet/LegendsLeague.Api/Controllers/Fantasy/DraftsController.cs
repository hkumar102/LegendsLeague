using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Application.Features.Fantasy.Drafts.Commands.CreateDraft;
using LegendsLeague.Application.Features.Fantasy.Drafts.Commands.UpdateDraft;
using LegendsLeague.Application.Features.Fantasy.Drafts.Commands.StartDraft;
using LegendsLeague.Application.Features.Fantasy.Drafts.Queries.GetDraftById;
using LegendsLeague.Application.Features.Fantasy.Drafts.Queries.ListDraftsByLeague;

namespace LegendsLeague.Api.Controllers.Fantasy;

/// <summary>Draft management endpoints for fantasy leagues.</summary>
[ApiController]
[Route("api/v1")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class DraftsController : ControllerBase
{
    private readonly ISender _sender;

    public DraftsController(ISender sender) => _sender = sender;

    /// <summary>Create a draft for a league.</summary>
    [HttpPost("leagues/{leagueId:guid}/drafts")]
    [ProducesResponseType(typeof(DraftDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DraftDto>> CreateDraft(
        [FromRoute] Guid leagueId,
        [FromBody] CreateDraftRequest body,
        CancellationToken ct = default)
    {
        var dto = await _sender.Send(new CreateDraftCommand(
            LeagueId: leagueId,
            DraftType: body.DraftType,
            ScheduledAtUtc: body.ScheduledAtUtc
        ), ct);

        return CreatedAtAction(nameof(GetDraftById), new { draftId = dto.Id }, dto);
    }

    /// <summary>Update a scheduled draft.</summary>
    [HttpPut("drafts/{draftId:guid}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDraft(
        [FromRoute] Guid draftId,
        [FromBody] UpdateDraftRequest body,
        CancellationToken ct = default)
    {
        var ok = await _sender.Send(new UpdateDraftCommand(
            DraftId: draftId,
            DraftType: body.DraftType,
            ScheduledAtUtc: body.ScheduledAtUtc
        ), ct);

        if (!ok) return NotFound();
        return NoContent();
    }

    /// <summary>Start a scheduled draft.</summary>
    [HttpPost("drafts/{draftId:guid}/start")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> StartDraft(
        [FromRoute] Guid draftId,
        CancellationToken ct = default)
    {
        var ok = await _sender.Send(new StartDraftCommand(draftId), ct);
        if (!ok) return NotFound();
        return NoContent();
    }

    /// <summary>Get a draft by id.</summary>
    [HttpGet("drafts/{draftId:guid}")]
    [ProducesResponseType(typeof(DraftDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DraftDto>> GetDraftById(
        [FromRoute] Guid draftId,
        CancellationToken ct = default)
    {
        var dto = await _sender.Send(new GetDraftByIdQuery(draftId), ct);
        if (dto is null) return NotFound();
        return Ok(dto);
    }

    /// <summary>List drafts in a league (paginated).</summary>
    [HttpGet("leagues/{leagueId:guid}/drafts")]
    [ProducesResponseType(typeof(PaginatedResult<DraftDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<DraftDto>>> ListDraftsByLeague(
        [FromRoute] Guid leagueId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? sort = null,
        CancellationToken ct = default)
    {
        var result = await _sender.Send(new ListDraftsByLeagueQuery(
            LeagueId: leagueId, Page: page, PageSize: pageSize, Sort: sort
        ), ct);

        return Ok(result);
    }
}
