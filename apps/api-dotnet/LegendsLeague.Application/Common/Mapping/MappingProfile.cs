using AutoMapper;

// Domain aliases
using DomainSeries = LegendsLeague.Domain.Entities.Fixtures.Series;
using DomainRealTeam = LegendsLeague.Domain.Entities.Fixtures.RealTeam;
using DomainPlayer = LegendsLeague.Domain.Entities.Fixtures.Player;
using DomainRole = LegendsLeague.Domain.Entities.Fixtures.Enums.PlayerRole;
using DomainBatting = LegendsLeague.Domain.Entities.Fixtures.Enums.BattingStyle;
using DomainBowling = LegendsLeague.Domain.Entities.Fixtures.Enums.BowlingStyle;

// Contract aliases
using SeriesDto = LegendsLeague.Contracts.Series.SeriesDto;
using RealTeamDto = LegendsLeague.Contracts.Teams.RealTeamDto;
using PlayerDto = LegendsLeague.Contracts.Players.PlayerDto;
using ContractRole = LegendsLeague.Contracts.Players.PlayerRole;
using ContractBatting = LegendsLeague.Contracts.Players.BattingStyle;
using ContractBowling = LegendsLeague.Contracts.Players.BowlingStyle;

namespace LegendsLeague.Application.Common.Mapping;

/// <summary>
/// Central AutoMapper profile to translate between Domain entities/enums and API Contracts.
/// Keep Domain and Contracts decoupled while making projections simple.
/// </summary>
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ---------- Entities → DTOs ----------
        CreateMap<DomainSeries, SeriesDto>();
        CreateMap<DomainRealTeam, RealTeamDto>();

        // Player → PlayerDto (enums handled via enum maps below)
        CreateMap<DomainPlayer, PlayerDto>();

        // ---------- Enums: Domain ↔ Contracts ----------
        CreateMap<DomainRole, ContractRole>()
            .ConvertUsing(src => (ContractRole)(int)src);
        CreateMap<ContractRole, DomainRole>()
            .ConvertUsing(src => (DomainRole)(int)src);

        CreateMap<DomainBatting, ContractBatting>()
            .ConvertUsing(src => (ContractBatting)(int)src);
        CreateMap<ContractBatting, DomainBatting>()
            .ConvertUsing(src => (DomainBatting)(int)src);

        CreateMap<DomainBowling, ContractBowling>()
            .ConvertUsing(src => (ContractBowling)(int)src);
        CreateMap<ContractBowling, DomainBowling>()
            .ConvertUsing(src => (DomainBowling)(int)src);
    }
}
