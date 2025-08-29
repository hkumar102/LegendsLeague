using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Queries.ListDraftsByLeague;

/// <summary>List drafts under a league (paginated).</summary>
public sealed record ListDraftsByLeagueQuery(
    Guid LeagueId,
    int Page = 1,
    int PageSize = 20,
    string? Sort = null
) : IRequest<PaginatedResult<DraftDto>>;
