using LegendsLeague.Domain.Entities.Fantasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsLeague.Infrastructure.Persistence.Fantasy.Configurations;

public sealed class FantasyLeagueConfiguration : IEntityTypeConfiguration<FantasyLeague>
{
    public void Configure(EntityTypeBuilder<FantasyLeague> b)
    {
        b.ToTable("leagues");
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).HasMaxLength(120).IsRequired();
        b.Property(x => x.MaxTeams).IsRequired();
        b.Property(x => x.SeriesId).IsRequired();
        b.HasIndex(x => new { x.SeriesId, x.Name });
    }
}
