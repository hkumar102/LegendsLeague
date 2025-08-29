using AutoMapper;
// Domain aliases
using DEntities = LegendsLeague.Domain.Entities.Fantasy;
using DEnums = LegendsLeague.Domain.Entities.Fantasy;
// Contracts aliases
using CFantasy = LegendsLeague.Contracts.Fantasy;
using CRosters = LegendsLeague.Contracts.Fantasy.Rosters;
using CScore = LegendsLeague.Contracts.Fantasy.Scoring;

namespace LegendsLeague.Application.Common.Mapping;

/// <summary>
/// AutoMapper profile for Fantasy schema:
/// - Maps Domain entities → Contract DTOs
/// - Bridges Domain enums ⇄ Contracts enums via name-based conversion (EnumMaps)
/// </summary>
public sealed class FantasyMappingProfile : Profile
{
    public FantasyMappingProfile()
    {
        // ===== Enums (name-based conversion via EnumMaps) =====
        // League member roles & status
        CreateMap<DEnums.LeagueMemberRole, CFantasy.LeagueMemberRole>()
            .ConvertUsing(src => EnumMaps.ToContract(src));
        CreateMap<CFantasy.LeagueMemberRole, DEnums.LeagueMemberRole>()
            .ConvertUsing(src => EnumMaps.ToDomain(src));

        CreateMap<DEnums.LeagueMemberStatus, CFantasy.LeagueMemberStatus>()
            .ConvertUsing(src => EnumMaps.ToContract(src));
        CreateMap<CFantasy.LeagueMemberStatus, DEnums.LeagueMemberStatus>()
            .ConvertUsing(src => EnumMaps.ToDomain(src));

        // Drafts
        CreateMap<DEnums.DraftType, CFantasy.DraftType>()
            .ConvertUsing(src => EnumMaps.ToContract(src));
        CreateMap<CFantasy.DraftType, DEnums.DraftType>()
            .ConvertUsing(src => EnumMaps.ToDomain(src));

        CreateMap<DEnums.DraftStatus, CFantasy.DraftStatus>()
            .ConvertUsing(src => EnumMaps.ToContract(src));
        CreateMap<CFantasy.DraftStatus, DEnums.DraftStatus>()
            .ConvertUsing(src => EnumMaps.ToDomain(src));

        CreateMap<DEnums.DraftPickStatus, CFantasy.DraftPickStatus>()
            .ConvertUsing(src => EnumMaps.ToContract(src));
        CreateMap<CFantasy.DraftPickStatus, DEnums.DraftPickStatus>()
            .ConvertUsing(src => EnumMaps.ToDomain(src));

        // Scoring type (optional/for future)
        CreateMap<DEnums.ScoringType, CFantasy.ScoringType>()
            .ConvertUsing(src => EnumMaps.ToContract(src));
        CreateMap<CFantasy.ScoringType, DEnums.ScoringType>()
            .ConvertUsing(src => EnumMaps.ToDomain(src));

        // Rosters
        CreateMap<DEnums.RosterSlot, CFantasy.RosterSlot>()
            .ConvertUsing(src => EnumMaps.ToContract(src));
        CreateMap<CFantasy.RosterSlot, DEnums.RosterSlot>()
            .ConvertUsing(src => EnumMaps.ToDomain(src));

        CreateMap<DEnums.RosterStatus, CFantasy.RosterStatus>()
            .ConvertUsing(src => EnumMaps.ToContract(src));
        CreateMap<CFantasy.RosterStatus, DEnums.RosterStatus>()
            .ConvertUsing(src => EnumMaps.ToDomain(src));

        // ===== Entities → DTOs =====
        // NOTE: Adjust entity/DTO types below if your actual names differ.
        CreateMap<DEntities.FantasyLeague, CFantasy.FantasyLeagueDto>();
        CreateMap<DEntities.LeagueMember, CFantasy.LeagueMemberDto>();
        CreateMap<DEntities.LeagueTeam, CFantasy.LeagueTeamDto>();
        CreateMap<DEntities.Draft, CFantasy.DraftDto>();
        CreateMap<DEntities.DraftPick, CFantasy.DraftPickDto>();

        // Rosters
        CreateMap<DEntities.RosterPlayer, CRosters.RosterPlayerDto>();

        // Scoring (if/when you expose via API)
        CreateMap<DEntities.FantasyScore, CScore.FantasyScoreDto>();
        CreateMap<DEntities.TeamFixtureScore, CScore.TeamFixtureScoreDto>();
        // PlayerMatchStats lives in Fixtures domain; map in a Fixtures profile when needed
    }
}