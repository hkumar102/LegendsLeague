using AutoMapper;
using AutoMapper.QueryableExtensions;

using LegendsLeague.Contracts.Teams;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fixtures.Teams.Queries;

/// <summary>Query to fetch a single team by id.</summary>
public sealed record GetTeamByIdQuery(Guid Id) : IRequest<RealTeamDto?>;

public sealed class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, RealTeamDto?>
{
    private readonly IFixturesDbContext _db;
    private readonly IMapper _mapper;

    public GetTeamByIdQueryHandler(IFixturesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<RealTeamDto?> Handle(GetTeamByIdQuery request, CancellationToken ct)
    {
        return await _db.RealTeams
            .AsNoTracking()
            .Where(t => t.Id == request.Id)
            .ProjectTo<RealTeamDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);
    }
}
