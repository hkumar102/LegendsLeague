using System.Net.Mime;
using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Application.Features.Fixtures.Teams.Commands.DeleteTeam;
using LegendsLeague.Application.Features.Fixtures.Teams.Commands.UpdateTeam;
using LegendsLeague.Application.Features.Fixtures.Teams.Queries;
using LegendsLeague.Contracts.Teams;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LegendsLeague.Api.Controllers.Fixtures
{
    /// <summary>
    /// Endpoints for individual team resources.
    /// </summary>
    [ApiController]
    [Route("api/v1")]
    [Produces(MediaTypeNames.Application.Json)]
    public sealed class TeamsController : ControllerBase
    {
        private readonly ISender _sender;
        public TeamsController(ISender sender) => _sender = sender;

        /// <summary>
        /// Gets a single team by its identifier.
        /// </summary>
        [HttpGet("teams/{id:guid}")]
        [ProducesResponseType(typeof(RealTeamDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RealTeamDto>> GetById([FromRoute] Guid id, CancellationToken ct = default)
        {
            var dto = await _sender.Send(new GetTeamByIdQuery(id), ct);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        /// <summary>
        /// Updates an existing team.
        /// </summary>
        [HttpPut("teams/{id:guid}")]
        [ProducesResponseType(typeof(RealTeamDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RealTeamDto>> Update(
            [FromRoute] Guid id,
            [FromBody] LegendsLeague.Contracts.Teams.TeamUpdateRequest body,
            CancellationToken ct = default)
        {
            try
            {
                var dto = await _sender.Send(new UpdateTeamCommand(id, body.Name, body.ShortName), ct);
                return Ok(dto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ConflictException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a team (soft delete via interceptor).
        /// </summary>
        [HttpDelete("teams/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct = default)
        {
            try
            {
                await _sender.Send(new DeleteTeamCommand(id), ct);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
