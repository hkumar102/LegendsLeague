using FixturesEntities = LegendsLeague.Domain.Entities.Fixtures;
using MediatR;

namespace LegendsLeague.Application.Features.Series.Queries;

public record GetAllSeriesQuery() : IRequest<IEnumerable<FixturesEntities.Series>>;

public class GetAllSeriesQueryHandler : IRequestHandler<GetAllSeriesQuery, IEnumerable<FixturesEntities.Series>>
{
    public Task<IEnumerable<FixturesEntities.Series>> Handle(GetAllSeriesQuery request, CancellationToken cancellationToken)
    {
        // Temporary static data (later replace with DB)
        var list = new[]
        {
            new FixturesEntities.Series { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Indian Premier League", SeasonYear = 2026 },
            new FixturesEntities.Series { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "ICC T20 World Cup", SeasonYear = 2026 }
        };
        return Task.FromResult<IEnumerable<FixturesEntities.Series>>(list);
    }
}
