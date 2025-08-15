using AutoMapper;

namespace LegendsLeague.Application.Common.Mapping;

/// <summary>
/// AutoMapper profile for Application layer DTO mappings.
/// </summary>
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Fixtures: Series -> SeriesDto
        CreateMap<LegendsLeague.Domain.Entities.Fixtures.Series, LegendsLeague.Contracts.Series.SeriesDto>();

        // Fixtures: RealTeam -> RealTeamDto
        CreateMap<LegendsLeague.Domain.Entities.Fixtures.RealTeam, LegendsLeague.Contracts.Teams.RealTeamDto>();
    }
}
