using LegendsLeague.Domain.Abstractions.Persistence;
using LegendsLeague.Domain.Entities.Fantasy;
using LegendsLeague.Infrastructure.Persistence.Extensions;
using LegendsLeague.Infrastructure.Persistence.ModelBuilding;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Infrastructure.Persistence.Fantasy;

/// <summary>
/// EF Core DbContext for the Fantasy bounded context (schema: <c>fantasy</c>).
/// Applies snake_case naming and global soft-delete filters.
/// </summary>
public sealed class FantasyDbContext : DbContext, IFantasyDbContext
{
    /// <summary>ctor</summary>
    public FantasyDbContext(DbContextOptions<FantasyDbContext> options) : base(options) { }

    public DbSet<FantasyLeague> Leagues => Set<FantasyLeague>();
    public DbSet<LeagueMember> LeagueMembers => Set<LeagueMember>();
    public DbSet<LeagueTeam> LeagueTeams => Set<LeagueTeam>();
    public DbSet<Draft> Drafts => Set<Draft>();
    public DbSet<DraftPick> DraftPicks => Set<DraftPick>();
    public DbSet<RosterPlayer> RosterPlayers => Set<RosterPlayer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("fantasy");
        // only apply the Fantasy configs
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FantasyDbContext).Assembly,
            t => t.Namespace != null &&
                t.Namespace.Contains("Persistence.Fantasy.Configurations", StringComparison.Ordinal));
        modelBuilder.UseSnakeCaseNames();
        modelBuilder.ApplySoftDeleteQueryFilters();
        base.OnModelCreating(modelBuilder);
    }
}
