using LegendsLeague.Application.Features.Series.Queries;
using LegendsLeague.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LegendsLeague.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SeriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SeriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Series>>> Get()
    {
        var result = await _mediator.Send(new GetAllSeriesQuery());
        return Ok(result);
    }
}
