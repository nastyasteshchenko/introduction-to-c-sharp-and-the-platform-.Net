using Nsu.Hackathon.Problem.TeamBuilding;

namespace Nsu.Hackathon.Problem.Hackathon;

public class HackathonTeam
{
    public long Id { get; set; }
    public long HackathonId { get; set; }
    public HackathonEntity HackathonEntity { get; set; } = null!;
    public long TeamId { get; set; }
    public Team Team { get; set; } = null!;
}