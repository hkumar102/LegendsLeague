using LegendsLeague.Domain.Entities.Fixtures;
using LegendsLeague.Domain.Entities.Fixtures.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsLeague.Infrastructure.Persistence.Fixtures.Configurations;

/// <summary>
/// Fluent configuration for the <see cref="Player"/> entity in the "fixtures" schema.
/// </summary>
public sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> b)
    {
        // Table
        b.ToTable("players");
        b.HasKey(x => x.Id);

        // Properties
        b.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(160);

        b.Property(x => x.ShortName)
            .HasMaxLength(60);

        b.Property(x => x.Country)
            .HasMaxLength(80);

        // Enums -> store as int for portability/versioning
        b.Property(x => x.Role)
            .HasConversion<int>()
            .IsRequired();

        b.Property(x => x.Batting)
            .HasConversion<int>()
            .IsRequired();

        b.Property(x => x.Bowling)
            .HasConversion<int>()
            .IsRequired();

        // Relationships
        b.HasOne(x => x.Series)
            .WithMany() // add Series.Players later if desired
            .HasForeignKey(x => x.SeriesId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.RealTeam)
            .WithMany() // add RealTeam.Players later if desired
            .HasForeignKey(x => x.RealTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes for lookups/search
        b.HasIndex(x => new { x.SeriesId, x.RealTeamId })
            .HasDatabaseName("ix_players_seriesid_teamid");

        b.HasIndex(x => x.FullName)
            .HasDatabaseName("ix_players_full_name");
    }
}
