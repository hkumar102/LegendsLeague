using LegendsLeague.Contracts.Common;
using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Queries.ListPicksByDraft;

/// <summary>List picks within a draft (paginated).</summary>
public sealed record ListPicksByDraftQuery(
    Guid DraftId,
    int Page = 1,
    int PageSize = 50,
    string? Sort = null
) : IRequest<PaginatedResult<DraftPickDto>>;
