using LegendsLeague.Domain.Entities.Fantasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsLeague.Infrastructure.Persistence.Fantasy.Configurations;

public sealed class DraftPickConfiguration : IEntityTypeConfiguration<DraftPick>
{
    public void Configure(EntityTypeBuilder<DraftPick> b)
    {
        // Table
        b.ToTable("draft_picks");
        b.HasKey(p => p.Id);

        // Relationships (same DbContext)
        b.HasOne(p => p.Draft)
            .WithMany(d => d.Picks)
            .HasForeignKey(p => p.DraftId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasOne(p => p.LeagueTeam)
            .WithMany(t => t.Picks)
            .HasForeignKey(p => p.LeagueTeamId)
            .OnDelete(DeleteBehavior.Cascade);

        // Scalar props
        b.Property(p => p.OverallPickNumber).IsRequired();
        b.Property(p => p.RoundNumber).IsRequired();
        b.Property(p => p.PickInRound).IsRequired();

        // Enum stored as int
        b.Property(p => p.Status)
            .HasConversion<int>()
            .IsRequired();

        // PlayerId is a scalar here (no cross-context FK)
        b.Property(p => p.PlayerId).IsRequired();

        // Timestamps
        b.Property(p => p.PickedAtUtc);

        // Uniques & indexes
        b.HasIndex(p => new { p.DraftId, p.OverallPickNumber })
            .IsUnique()
            .HasDatabaseName("ux_draft_picks_draft_overall");

        b.HasIndex(p => new { p.DraftId, p.PlayerId })
            .IsUnique()
            .HasDatabaseName("ux_draft_picks_draft_player");

        b.HasIndex(p => p.LeagueTeamId)
            .HasDatabaseName("ix_draft_picks_league_team");
    }
}
