using LegendsLeague.Contracts.Fantasy;
using MediatR;

namespace LegendsLeague.Application.Features.Fantasy.Drafts.Queries.GetDraftById;

/// <summary>Get a draft by id.</summary>
public sealed record GetDraftByIdQuery(Guid DraftId) : IRequest<DraftDto?>;
