using System.Net.Mime;
using LegendsLeague.Application.Features.Fixtures.Fixtures.Commands.CreateFixture;
using LegendsLeague.Application.Features.Fixtures.Fixtures.Queries;
using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LegendsLeague.Api.Controllers.Fixtures
{
    /// <summary>
    /// Series-scoped fixtures endpoints (list/search and create).
    /// </summary>
    [ApiController]
    [Route("api/v1/series/{seriesId:guid}/fixtures")]
    [Produces(MediaTypeNames.Application.Json)]
    public sealed class SeriesFixturesController : ControllerBase
    {
        private readonly ISender _sender;
        public SeriesFixturesController(ISender sender) => _sender = sender;

        /// <summary>Lists fixtures in a series (paged; optional date range).</summary>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<FixtureDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<FixtureDto>>> GetBySeries(
            [FromRoute] Guid seriesId,
            [FromQuery] DateTimeOffset? fromUtc,
            [FromQuery] DateTimeOffset? toUtc,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            var result = await _sender.Send(new GetFixturesBySeriesQuery(
                SeriesId: seriesId,
                FromUtc: fromUtc,
                ToUtc: toUtc,
                Page: page,
                PageSize: pageSize
            ), ct);

            return Ok(result);
        }

        /// <summary>Lists fixtures for a specific team in a series (paged; optional date range).</summary>
        [HttpGet("teams/{teamId:guid}")]
        [ProducesResponseType(typeof(PaginatedResult<FixtureDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<FixtureDto>>> GetByTeam(
            [FromRoute] Guid seriesId,
            [FromRoute] Guid teamId,
            [FromQuery] DateTimeOffset? fromUtc,
            [FromQuery] DateTimeOffset? toUtc,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            var result = await _sender.Send(new GetFixturesForTeamQuery(
                SeriesId: seriesId,
                TeamId: teamId,
                FromUtc: fromUtc,
                ToUtc: toUtc,
                Page: page,
                PageSize: pageSize
            ), ct);

            return Ok(result);
        }

        /// <summary>Creates a new fixture within a series.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(FixtureDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FixtureDto>> Create(
            [FromRoute] Guid seriesId,
            [FromBody] FixtureCreateRequest body,
            CancellationToken ct)
        {
            var created = await _sender.Send(new CreateFixtureCommand(
                SeriesId: seriesId,
                HomeTeamId: body.HomeTeamId,
                AwayTeamId: body.AwayTeamId,
                StartTimeUtc: body.StartTimeUtc
            ), ct);

            return CreatedAtAction(
                nameof(FixturesController.GetById),
                "Fixtures",
                new { id = created.Id },
                created
            );
        }
    }
}
