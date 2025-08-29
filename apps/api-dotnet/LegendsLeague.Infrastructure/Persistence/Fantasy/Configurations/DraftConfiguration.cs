using LegendsLeague.Domain.Entities.Fantasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsLeague.Infrastructure.Persistence.Fantasy.Configurations;

public sealed class DraftConfiguration : IEntityTypeConfiguration<Draft>
{
    public void Configure(EntityTypeBuilder<Draft> b)
    {
        // Table
        b.ToTable("drafts");
        b.HasKey(d => d.Id);

        // Relationships
        b.HasOne(d => d.League)
            .WithOne(l => l.Draft)
            .HasForeignKey<Draft>(d => d.LeagueId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasMany(d => d.Picks)
            .WithOne(p => p.Draft)
            .HasForeignKey(p => p.DraftId)
            .OnDelete(DeleteBehavior.Cascade);

        // Properties
        b.Property(d => d.DraftType)
            .HasConversion<int>() // store enum as int
            .IsRequired();

        b.Property(d => d.Status)
            .HasConversion<int>()
            .IsRequired();

        b.Property(d => d.ScheduledAtUtc);
        b.Property(d => d.StartedAtUtc);
        b.Property(d => d.CompletedAtUtc);
    }
}
