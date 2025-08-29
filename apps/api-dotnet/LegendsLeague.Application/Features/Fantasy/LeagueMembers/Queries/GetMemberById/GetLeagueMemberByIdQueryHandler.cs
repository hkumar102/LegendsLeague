using AutoMapper;
using AutoMapper.QueryableExtensions;

using LegendsLeague.Contracts.Fantasy;
using LegendsLeague.Domain.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Queries.GetMemberById;

/// <summary>Handles GetLeagueMemberByIdQuery.</summary>
public sealed class GetLeagueMemberByIdQueryHandler : IRequestHandler<GetLeagueMemberByIdQuery, LeagueMemberDto?>
{
    private readonly IFantasyDbContext _db;
    private readonly IMapper _mapper;

    public GetLeagueMemberByIdQueryHandler(IFantasyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<LeagueMemberDto?> Handle(GetLeagueMemberByIdQuery request, CancellationToken ct)
    {
        return await _db.LeagueMembers.AsNoTracking()
            .Where(m => m.Id == request.MemberId)
            .ProjectTo<LeagueMemberDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);
    }
}
