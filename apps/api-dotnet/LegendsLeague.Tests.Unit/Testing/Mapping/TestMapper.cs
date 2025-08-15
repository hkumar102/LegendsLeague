using AutoMapper;
using LegendsLeague.Application.Common.Mapping;

namespace LegendsLeague.Tests.Unit.Testing.Mapping;

/// <summary>
/// Provides a test IMapper instance configured with the Application MappingProfile.
/// </summary>
public static class TestMapper
{
    public static IMapper Create()
    {
        var cfg = new MapperConfiguration(c =>
        {
            c.AddProfile(new MappingProfile());
        });
        cfg.AssertConfigurationIsValid();
        return cfg.CreateMapper();
    }
}
