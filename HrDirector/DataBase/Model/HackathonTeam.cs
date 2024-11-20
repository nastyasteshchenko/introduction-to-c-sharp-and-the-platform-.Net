using Nsu.Hackathon.Problem.Common.Model;

namespace Nsu.Hackathon.Problem.HrDirector.DataBase.Model;

public class HackathonTeam
{
    public long Id { get; set; }
    public long HackathonId { get; set; }
    public HackathonEntity HackathonEntity { get; set; } = null!;
    public long TeamId { get; set; }
    public Team Team { get; set; } = null!;
}