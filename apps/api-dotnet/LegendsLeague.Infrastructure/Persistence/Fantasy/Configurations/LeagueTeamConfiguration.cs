using LegendsLeague.Domain.Entities.Fantasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegendsLeague.Infrastructure.Persistence.Fantasy.Configurations;

public sealed class LeagueTeamConfiguration : IEntityTypeConfiguration<LeagueTeam>
{
    public void Configure(EntityTypeBuilder<LeagueTeam> b)
    {
        // Table
        b.ToTable("league_teams");
        b.HasKey(t => t.Id);

        // Properties
        b.Property(t => t.Name)
            .HasMaxLength(120)
            .IsRequired();

        b.Property(t => t.LeagueId).IsRequired();
        b.Property(t => t.OwnerId).IsRequired();

        // Relationships (same DbContext)
        b.HasOne(t => t.League)
            .WithMany(l => l.Teams)
            .HasForeignKey(t => t.LeagueId)
            .OnDelete(DeleteBehavior.Cascade);

        // Owner is a LeagueMember. We don't need a back-collection on LeagueMember.
        b.HasOne(t => t.Owner)
            .WithMany() // no nav on LeagueMember
            .HasForeignKey(t => t.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Uniques & indexes
        // One team name per league
        b.HasIndex(t => new { t.LeagueId, t.Name })
            .IsUnique()
            .HasDatabaseName("ux_league_teams_league_name");

        // One team per owner per league
        b.HasIndex(t => new { t.LeagueId, t.OwnerId })
            .IsUnique()
            .HasDatabaseName("ux_league_teams_league_owner");
    }
}
