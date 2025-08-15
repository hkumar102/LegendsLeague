using System.Net.Mime;
using LegendsLeague.Contracts.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LegendsLeague.Application.Features.Fixtures.Fixtures.Commands.DeleteFixture;
using LegendsLeague.Application.Features.Fixtures.Fixtures.Commands.UpdateFixture;
using LegendsLeague.Application.Features.Fixtures.Fixtures.Queries;

namespace LegendsLeague.Api.Controllers.Fixtures
{
    /// <summary>
    /// Fixture resource endpoints (by id: get/update/delete).
    /// </summary>
    [ApiController]
    [Route("api/v1/fixtures")]
    [Produces(MediaTypeNames.Application.Json)]
    public sealed class FixturesController : ControllerBase
    {
        private readonly ISender _sender;
        public FixturesController(ISender sender) => _sender = sender;

        /// <summary>Gets a fixture by id.</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(FixtureDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FixtureDto>> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var dto = await _sender.Send(new GetFixtureByIdQuery(id), ct);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        /// <summary>Updates a fixture.</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(FixtureDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FixtureDto>> Update([FromRoute] Guid id, [FromBody] FixtureUpdateRequest body, CancellationToken ct)
        {
            var cmd = new UpdateFixtureCommand(
                Id: id,
                HomeTeamId: body.HomeTeamId,
                AwayTeamId: body.AwayTeamId,
                StartTimeUtc: body.StartTimeUtc
            );

            var dto = await _sender.Send(cmd, ct);
            return Ok(dto);
        }

        /// <summary>Deletes (soft) a fixture.</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            await _sender.Send(new DeleteFixtureCommand(id), ct);
            return NoContent();
        }
    }
}
