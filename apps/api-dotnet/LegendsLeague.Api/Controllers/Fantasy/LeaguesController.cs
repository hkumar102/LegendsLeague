using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Application.Features.Fantasy.Leagues.Commands.CreateLeague;
using LegendsLeague.Application.Features.Fantasy.Leagues.Commands.UpdateLeague;
using LegendsLeague.Application.Features.Fantasy.Leagues.Commands.DeleteLeague;
using LegendsLeague.Application.Features.Fantasy.Leagues.Queries;

namespace LegendsLeague.Api.Controllers.Fantasy
{
    /// <summary>
    /// Fantasy leagues: create/update/delete and read endpoints.
    /// </summary>
    [ApiController]
    [Route("api/v1/fantasy")]
    [Produces(MediaTypeNames.Application.Json)]
    public sealed class LeaguesController : ControllerBase
    {
        private readonly ISender _sender;

        public LeaguesController(ISender sender) => _sender = sender;

        /// <summary>Create a new fantasy league.</summary>
        [HttpPost("leagues")]
        [ProducesResponseType(typeof(FantasyLeagueDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FantasyLeagueDto>> CreateLeague(
            [FromBody] FantasyLeagueCreateRequest request,
            CancellationToken ct = default)
        {
            var dto = await _sender.Send(new CreateLeagueCommand(
                SeriesId: request.SeriesId,
                Name: request.Name,
                MaxTeams: request.MaxTeams,
                CommissionerUserId: request.CommissionerUserId
            ), ct);

            return CreatedAtAction(nameof(GetLeagueById), new { id = dto.Id }, dto);
        }

        /// <summary>Update an existing fantasy league.</summary>
        [HttpPut("leagues/{id:guid}")]
        [ProducesResponseType(typeof(FantasyLeagueDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FantasyLeagueDto>> UpdateLeague(
            [FromRoute] Guid id,
            [FromBody] FantasyLeagueUpdateRequest request,
            CancellationToken ct = default)
        {
            var dto = await _sender.Send(new UpdateLeagueCommand(
                Id: id,
                Name: request.Name,
                MaxTeams: request.MaxTeams
            ), ct);

            return Ok(dto);
        }

        /// <summary>Delete a fantasy league.</summary>
        [HttpDelete("leagues/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLeague([FromRoute] Guid id, CancellationToken ct = default)
        {
            var ok = await _sender.Send(new DeleteLeagueCommand(id), ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        /// <summary>Get a fantasy league by id.</summary>
        [HttpGet("leagues/{id:guid}")]
        [ProducesResponseType(typeof(FantasyLeagueDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FantasyLeagueDto>> GetLeagueById([FromRoute] Guid id, CancellationToken ct = default)
        {
            var dto = await _sender.Send(new GetLeagueByIdQuery(id), ct);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        /// <summary>List leagues for a series (paginated).</summary>
        [HttpGet("series/{seriesId:guid}/leagues")]
        [ProducesResponseType(typeof(PaginatedResult<FantasyLeagueDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<FantasyLeagueDto>>> GetLeaguesBySeries(
            [FromRoute] Guid seriesId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? sort = null,
            CancellationToken ct = default)
        {
            var result = await _sender.Send(new GetLeaguesBySeriesQuery(
                SeriesId: seriesId,
                Page: page,
                PageSize: pageSize,
                Sort: sort
            ), ct);

            return Ok(result);
        }

        /// <summary>Search leagues by name (paginated, optional series scope).</summary>
        [HttpGet("leagues:search")]
        [ProducesResponseType(typeof(PaginatedResult<FantasyLeagueDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaginatedResult<FantasyLeagueDto>>> SearchLeagues(
            [FromQuery] string search,
            [FromQuery] Guid? seriesId = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            var result = await _sender.Send(new SearchLeaguesQuery(
                Search: search,
                SeriesId: seriesId,
                Page: page,
                PageSize: pageSize
            ), ct);

            return Ok(result);
        }
    }
}
