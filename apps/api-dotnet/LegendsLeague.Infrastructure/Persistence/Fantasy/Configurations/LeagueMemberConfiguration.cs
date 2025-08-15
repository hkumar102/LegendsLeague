using LegendsLeague.Domain.Entities.Fantasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsLeague.Infrastructure.Persistence.Fantasy.Configurations;

public sealed class LeagueMemberConfiguration : IEntityTypeConfiguration<LeagueMember>
{
    public void Configure(EntityTypeBuilder<LeagueMember> b)
    {
        b.ToTable("league_members");
        b.HasKey(x => x.Id);
        b.Property(x => x.LeagueId).IsRequired();
        b.Property(x => x.UserId).IsRequired();
        b.Property(x => x.Role).IsRequired();
        b.HasIndex(x => new { x.LeagueId, x.UserId }).IsUnique();
    }
}
