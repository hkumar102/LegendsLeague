using AutoMapper;
using LegendsLeague.Application.Abstractions.Persistence;
using LegendsLeague.Contracts.Fantasy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.Leagues.Commands.UpdateLeague;

public sealed class UpdateLeagueCommandHandler : IRequestHandler<UpdateLeagueCommand, FantasyLeagueDto>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public UpdateLeagueCommandHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<FantasyLeagueDto> Handle(UpdateLeagueCommand request, CancellationToken ct)
    {
        var league = await _db.Leagues.FirstOrDefaultAsync(l => l.Id == request.Id, ct);
        if (league is null)
            throw new KeyNotFoundException("League not found.");

        if (!string.IsNullOrWhiteSpace(request.Name) && !request.Name.Equals(league.Name, StringComparison.OrdinalIgnoreCase))
        {
            var dup = await _db.Leagues
                .AsNoTracking()
                .AnyAsync(l => l.SeriesId == league.SeriesId &&
                               l.Name.ToLower() == request.Name!.ToLower() &&
                               l.Id != league.Id, ct);

            if (dup)
                throw new InvalidOperationException("Another league with this name already exists for this series.");

            league.Name = request.Name!.Trim();
        }

        if (request.MaxTeams.HasValue)
            league.MaxTeams = request.MaxTeams.Value;

        await _db.SaveChangesAsync(ct);
        return _mapper.Map<FantasyLeagueDto>(league);
    }
}
