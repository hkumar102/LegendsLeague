using Microsoft.AspNetCore.Mvc;

namespace LegendsLeague.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SeriesController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<SeriesDto>> Get()
    {
        // Temporary stub: in-memory sample (we'll replace with DB/module later)
        var list = new[]
        {
            new SeriesDto(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Indian Premier League", 2026),
            new SeriesDto(Guid.Parse("22222222-2222-2222-2222-222222222222"), "ICC T20 World Cup", 2026)
        };
        return Ok(list);
    }
}

public record SeriesDto(Guid Id, string Name, int SeasonYear);
