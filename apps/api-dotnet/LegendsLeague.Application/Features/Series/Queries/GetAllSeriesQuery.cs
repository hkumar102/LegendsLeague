using LegendsLeague.Domain.Entities;
using MediatR;

namespace LegendsLeague.Application.Features.Series.Queries;

public record GetAllSeriesQuery() : IRequest<IEnumerable<Series>>;

public class GetAllSeriesQueryHandler : IRequestHandler<GetAllSeriesQuery, IEnumerable<Series>>
{
    public Task<IEnumerable<Series>> Handle(GetAllSeriesQuery request, CancellationToken cancellationToken)
    {
        // Temporary static data (later replace with DB)
        var list = new[]
        {
            new Series { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Indian Premier League", SeasonYear = 2026 },
            new Series { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "ICC T20 World Cup", SeasonYear = 2026 }
        };
        return Task.FromResult<IEnumerable<Series>>(list);
    }
}
