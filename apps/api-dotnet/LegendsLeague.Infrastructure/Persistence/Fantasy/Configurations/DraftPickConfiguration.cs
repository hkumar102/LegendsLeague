using LegendsLeague.Domain.Entities.Fantasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsLeague.Infrastructure.Persistence.Fantasy.Configurations;

public sealed class DraftPickConfiguration : IEntityTypeConfiguration<DraftPick>
{
    public void Configure(EntityTypeBuilder<DraftPick> b)
    {
        b.ToTable("draft_picks");
        b.HasKey(x => x.Id);
        b.Property(x => x.DraftId).IsRequired();
        b.Property(x => x.LeagueTeamId).IsRequired();
        b.Property(x => x.PlayerId).IsRequired();
        b.HasIndex(x => new { x.DraftId, x.RoundNo, x.PickNo });
        b.HasIndex(x => new { x.LeagueTeamId, x.DraftId });
    }
}
