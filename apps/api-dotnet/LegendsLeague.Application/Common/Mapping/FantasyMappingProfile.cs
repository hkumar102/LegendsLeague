using AutoMapper;
using D = LegendsLeague.Domain.Entities.Fantasy;
using C = LegendsLeague.Contracts.Fantasy;

namespace LegendsLeague.Application.Common.Mapping;

/// <summary>
/// AutoMapper profile for Fantasy schema:
/// Maps Domain entities ⇄ Contract DTOs and aligns enum types for wire safety.
/// </summary>
public sealed class FantasyMappingProfile : Profile
{
    public FantasyMappingProfile()
    {
        // ===== Enums =====
        CreateMap<D.LeagueMemberRole, C.LeagueMemberRole>()
            .ConvertUsing(src => (C.LeagueMemberRole)src);
        CreateMap<C.LeagueMemberRole, D.LeagueMemberRole>()
            .ConvertUsing(src => (D.LeagueMemberRole)src);

        CreateMap<D.DraftType, C.DraftType>()
            .ConvertUsing(src => (C.DraftType)src);
        CreateMap<C.DraftType, D.DraftType>()
            .ConvertUsing(src => (D.DraftType)src);

        CreateMap<D.DraftStatus, C.DraftStatus>()
            .ConvertUsing(src => (C.DraftStatus)src);
        CreateMap<C.DraftStatus, D.DraftStatus>()
            .ConvertUsing(src => (D.DraftStatus)src);

        CreateMap<D.RosterSlot, C.RosterSlot>()
            .ConvertUsing(src => (C.RosterSlot)src);
        CreateMap<C.RosterSlot, D.RosterSlot>()
            .ConvertUsing(src => (D.RosterSlot)src);

        // ===== Entities → DTOs =====
        CreateMap<D.FantasyLeague, C.FantasyLeagueDto>();
        CreateMap<D.LeagueMember,  C.LeagueMemberDto>();
        CreateMap<D.LeagueTeam,    C.LeagueTeamDto>();
        CreateMap<D.Draft,         C.DraftDto>();
        CreateMap<D.DraftPick,     C.DraftPickDto>();
        CreateMap<D.RosterPlayer,  C.RosterPlayerDto>();
    }
}