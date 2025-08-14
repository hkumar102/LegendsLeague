using LegendsLeague.Domain.Entities.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsLeague.Infrastructure.Persistence.Fixtures.Configurations;

public class SeriesConfiguration : IEntityTypeConfiguration<Series>
{
    public void Configure(EntityTypeBuilder<Series> b)
    {
        b.ToTable("series");
        b.HasKey(x => x.Id);

        b.Property(x => x.Name).HasMaxLength(200).IsRequired();
        b.Property(x => x.SeasonYear).IsRequired();

        // Optional seed to verify wiring (can remove later)
        b.HasData(
            new Series { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Indian Premier League", SeasonYear = 2026 },
            new Series { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "ICC T20 World Cup", SeasonYear = 2026 }
        );
    }
}
