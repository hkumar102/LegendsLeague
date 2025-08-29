using LegendsLeague.Domain.Entities.Fantasy;
using Microsoft.EntityFrameworkCore;

namespace LegendsLeague.Domain.Abstractions.Persistence;

/// <summary>
/// Abstraction over the Fantasy persistence surface used by the Application layer.
/// </summary>
public interface IFantasyDbContext
{
    DbSet<FantasyLeague> Leagues { get; }
    DbSet<LeagueMember> LeagueMembers { get; }
    DbSet<LeagueTeam> LeagueTeams { get; }
    DbSet<Draft> Drafts { get; }
    DbSet<DraftPick> DraftPicks { get; }
    DbSet<RosterPlayer> RosterPlayers { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
