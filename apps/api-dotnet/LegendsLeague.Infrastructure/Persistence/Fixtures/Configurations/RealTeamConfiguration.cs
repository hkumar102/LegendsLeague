using LegendsLeague.Domain.Entities.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsLeague.Infrastructure.Persistence.Fixtures.Configurations;

public class RealTeamConfiguration : IEntityTypeConfiguration<RealTeam>
{
    public void Configure(EntityTypeBuilder<RealTeam> b)
    {
        b.ToTable("real_teams");
        b.HasKey(x => x.Id);

        b.Property(x => x.Name).HasMaxLength(120).IsRequired();
        b.Property(x => x.ShortName).HasMaxLength(10);

        b.HasOne(x => x.Series)
         .WithMany(s => s.RealTeams)
         .HasForeignKey(x => x.SeriesId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}
