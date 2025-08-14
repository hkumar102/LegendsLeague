using LegendsLeague.Domain.Common;

namespace LegendsLeague.Domain.Entities.Fixtures;

public class Series : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SeasonYear { get; set; }

    public ICollection<RealTeam> RealTeams { get; set; } = new List<RealTeam>();
    public ICollection<Fixture> Fixtures { get; set; } = new List<Fixture>();
}
