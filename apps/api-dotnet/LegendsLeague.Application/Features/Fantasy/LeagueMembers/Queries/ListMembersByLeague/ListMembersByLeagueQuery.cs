using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.LeagueMembers.Queries.ListMembersByLeague;

/// <summary>List members for a league (paginated).</summary>
public sealed record ListMembersByLeagueQuery(
    Guid LeagueId,
    int Page = 1,
    int PageSize = 20,
    string? Sort = null
) : IRequest<PaginatedResult<LeagueMemberDto>>;
