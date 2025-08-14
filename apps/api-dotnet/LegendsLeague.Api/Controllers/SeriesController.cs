using LegendsLeague.Application.Features.Series.Queries;
using LegendsLeague.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LegendsLeague.Api.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class SeriesController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public SeriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Series>>> Get()
    {
        var result = await _mediator.Send(new GetAllSeriesQuery());
        return Ok(result);
    }
}
