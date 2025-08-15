using LegendsLeague.Domain.Entities.Fantasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsLeague.Infrastructure.Persistence.Fantasy.Configurations;

public sealed class DraftConfiguration : IEntityTypeConfiguration<Draft>
{
    public void Configure(EntityTypeBuilder<Draft> b)
    {
        b.ToTable("drafts");
        b.HasKey(x => x.Id);
        b.Property(x => x.LeagueId).IsRequired();
        b.Property(x => x.Type).IsRequired();
        b.Property(x => x.Status).IsRequired();
        b.Property(x => x.StartsAtUtc).IsRequired();
        b.HasIndex(x => new { x.LeagueId, x.Status });
    }
}
