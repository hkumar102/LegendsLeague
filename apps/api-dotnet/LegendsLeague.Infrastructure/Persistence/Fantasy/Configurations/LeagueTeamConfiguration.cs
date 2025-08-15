using LegendsLeague.Domain.Entities.Fantasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsLeague.Infrastructure.Persistence.Fantasy.Configurations;

public sealed class LeagueTeamConfiguration : IEntityTypeConfiguration<LeagueTeam>
{
    public void Configure(EntityTypeBuilder<LeagueTeam> b)
    {
        b.ToTable("league_teams");
        b.HasKey(x => x.Id);
        b.Property(x => x.LeagueId).IsRequired();
        b.Property(x => x.Name).HasMaxLength(80).IsRequired();
        b.Property(x => x.OwnerUserId).IsRequired();
        b.HasIndex(x => new { x.LeagueId, x.Name });
        b.HasIndex(x => new { x.LeagueId, x.OwnerUserId });
    }
}
