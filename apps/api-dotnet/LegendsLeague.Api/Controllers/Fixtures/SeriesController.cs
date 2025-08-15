using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LegendsLeague.Application.Features.Fixtures.Series.Queries;
using LegendsLeague.Contracts.Series;
using LegendsLeague.Contracts.Common;

// Teams (series-scoped)
using LegendsLeague.Contracts.Teams;
using LegendsLeague.Application.Common.Exceptions;
using LegendsLeague.Application.Features.Fixtures.Teams.Queries;
using LegendsLeague.Application.Features.Fixtures.Teams.Commands.CreateTeam;

namespace LegendsLeague.Api.Controllers.Fixtures
{
    /// <summary>
    /// Read endpoints for real-world cricket series (e.g., IPL seasons) and series-scoped resources.
    /// </summary>
    [ApiController]
    [Route("api/v1/series")]
    [Produces(MediaTypeNames.Application.Json)]
    public sealed class SeriesController : ControllerBase
    {
        private readonly ISender _sender;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeriesController"/>.
        /// </summary>
        /// <param name="sender">MediatR sender used to dispatch queries/commands to the Application layer.</param>
        public SeriesController(ISender sender) => _sender = sender;

        /// <summary>
        /// Lists series with optional filtering, sorting, and pagination.
        /// </summary>
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
            var result = await _sender.Send(new GetSeriesListQuery(
                SeasonYear: seasonYear,
                Search: search,
                Page: page,
                PageSize: pageSize,
                Sort: sort
            ), ct);

            return Ok(result);
        }

        /// <summary>
        /// Gets details for a single series by its identifier.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(SeriesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SeriesDto>> GetSeriesById([FromRoute] Guid id, CancellationToken ct = default)
        {
            var dto = await _sender.Send(new GetSeriesByIdQuery(id), ct);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        // ---------------------------
        // SERIES-SCOPED TEAMS
        // ---------------------------

        /// <summary>
        /// Lists teams in a series with optional search, sort and pagination.
        /// </summary>
        [HttpGet("{seriesId:guid}/teams")]
        [ProducesResponseType(typeof(PaginatedResult<RealTeamDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaginatedResult<RealTeamDto>>> GetTeamsBySeries(
            [FromRoute] Guid seriesId,
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? sort = null,
            CancellationToken ct = default)
        {
            var result = await _sender.Send(new GetTeamsBySeriesQuery(seriesId, search, page, pageSize, sort), ct);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new team inside a series.
        /// </summary>
        [HttpPost("{seriesId:guid}/teams")]
        [ProducesResponseType(typeof(RealTeamDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RealTeamDto>> CreateTeamInSeries(
            [FromRoute] Guid seriesId,
            [FromBody] TeamCreateRequest body,
            CancellationToken ct = default)
        {
            try
            {
                var dto = await _sender.Send(new CreateTeamCommand(seriesId, body.Name, body.ShortName), ct);
                // Location can point to the team resource itself
                return CreatedAtAction(
                    actionName: nameof(LegendsLeague.Api.Controllers.Fixtures.TeamsController.GetById),
                    routeValues: new { id = dto.Id },
                    value: dto);
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
