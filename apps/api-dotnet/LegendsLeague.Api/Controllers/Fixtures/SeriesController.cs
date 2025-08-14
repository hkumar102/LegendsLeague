using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LegendsLeague.Application.Features.Fixtures.Queries;
using LegendsLeague.Contracts.Series;

namespace LegendsLeague.Api.Controllers.Fixtures
{
    /// <summary>
    /// Read endpoints for real-world cricket series (e.g., IPL seasons).
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
        /// <param name="sender">MediatR sender used to dispatch queries to the Application layer.</param>
        public SeriesController(ISender sender) => _sender = sender;

        /// <summary>
        /// Lists series with optional filtering, sorting, and pagination.
        /// </summary>
        /// <param name="seasonYear">Optional exact season year (e.g., 2026).</param>
        /// <param name="search">Optional case-insensitive substring match on the series name.</param>
        /// <param name="page">1-based page number (default 1).</param>
        /// <param name="pageSize">Page size (default 20, max 100).</param>
        /// <param name="sort">Sort key: name, -name, seasonYear, -seasonYear (default seasonYear asc, then name).</param>
        /// <param name="ct">Cancellation token for the request.</param>
        /// <returns>A list of matching series.</returns>
        /// <response code="200">Returns the list of series.</response>
        /// <response code="400">Validation errors for query parameters.</response>
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
        /// <param name="id">Series identifier.</param>
        /// <param name="ct">Cancellation token for the request.</param>
        /// <returns>The series if found; otherwise 404.</returns>
        /// <response code="200">Returns the series.</response>
        /// <response code="404">Series not found.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(SeriesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SeriesDto>> GetSeriesById([FromRoute] Guid id, CancellationToken ct = default)
        {
            var dto = await _sender.Send(new GetSeriesByIdQuery(id), ct);
            if (dto is null) return NotFound();
            return Ok(dto);
        }
    }
}
