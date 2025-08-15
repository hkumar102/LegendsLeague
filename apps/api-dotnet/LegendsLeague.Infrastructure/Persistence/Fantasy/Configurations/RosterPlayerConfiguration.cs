using LegendsLeague.Domain.Entities.Fantasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsLeague.Infrastructure.Persistence.Fantasy.Configurations;

public sealed class RosterPlayerConfiguration : IEntityTypeConfiguration<RosterPlayer>
{
    public void Configure(EntityTypeBuilder<RosterPlayer> b)
    {
        b.ToTable("roster_players");
        b.HasKey(x => x.Id);
        b.Property(x => x.LeagueTeamId).IsRequired();
        b.Property(x => x.PlayerId).IsRequired();
        b.Property(x => x.Slot).IsRequired();
        b.HasIndex(x => new { x.LeagueTeamId, x.PlayerId, x.Slot });
    }
}
