using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LegendsLeague.Application.Features.Fixtures.Queries;
using LegendsLeague.Contracts.Series;
using LegendsLeague.Application.Features.Fixtures.Commands.CreateSeries;
using LegendsLeague.Application.Features.Fixtures.Commands.UpdateSeries;
using LegendsLeague.Application.Features.Fixtures.Commands.DeleteSeries;
using LegendsLeague.Application.Common.Exceptions;

namespace LegendsLeague.Api.Controllers.Fixtures
{
    /// <summary>
    /// Read/Write endpoints for real-world cricket series (e.g., IPL seasons).
    /// </summary>
    [ApiController]
    [Route("api/v1/series")]
    [Produces(MediaTypeNames.Application.Json)]
    public sealed class SeriesController : ControllerBase
    {
        private readonly ISender _sender;

        public SeriesController(ISender sender) => _sender = sender;

        /// <summary>Lists series with optional filtering, sorting, and pagination.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<SeriesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyList<SeriesDto>>> GetSeries(
            [FromQuery] int? seasonYear,
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? sort = null,
            CancellationToken ct = default)
        {
            var result = await _sender.Send(new GetSeriesListQuery(seasonYear, search, page, pageSize, sort), ct);
            return Ok(result);
        }

        /// <summary>Gets details for a single series by its identifier.</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(SeriesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SeriesDto>> GetSeriesById([FromRoute] Guid id, CancellationToken ct = default)
        {
            var dto = await _sender.Send(new GetSeriesByIdQuery(id), ct);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        /// <summary>Creates a new series.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(SeriesDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SeriesDto>> Create([FromBody] SeriesCreateRequest body, CancellationToken ct = default)
        {
            try
            {
                var dto = await _sender.Send(new CreateSeriesCommand(body.Name, body.SeasonYear), ct);
                return CreatedAtAction(nameof(GetSeriesById), new { id = dto.Id }, dto);
            }
            catch (ConflictException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }

        /// <summary>Updates an existing series.</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(SeriesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SeriesDto>> Update([FromRoute] Guid id, [FromBody] SeriesUpdateRequest body, CancellationToken ct = default)
        {
            try
            {
                var dto = await _sender.Send(new UpdateSeriesCommand(id, body.Name, body.SeasonYear), ct);
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

        /// <summary>Deletes a series (optionally forcing deletion when dependencies exist).</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, [FromQuery] bool force = false, CancellationToken ct = default)
        {
            try
            {
                await _sender.Send(new DeleteSeriesCommand(id, force), ct);
                return NoContent();
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
    }
}
