using LegendsLeague.Domain.Entities.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsLeague.Infrastructure.Persistence.Fixtures.Configurations;

public class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
{
    public void Configure(EntityTypeBuilder<Fixture> b)
    {
        b.ToTable("fixtures");
        b.HasKey(x => x.Id);

        b.Property(x => x.StartTimeUtc).IsRequired();

        b.HasOne(x => x.Series)
         .WithMany(s => s.Fixtures)
         .HasForeignKey(x => x.SeriesId)
         .OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.HomeTeam)
         .WithMany()
         .HasForeignKey(x => x.HomeTeamId)
         .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.AwayTeam)
         .WithMany()
         .HasForeignKey(x => x.AwayTeamId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}
