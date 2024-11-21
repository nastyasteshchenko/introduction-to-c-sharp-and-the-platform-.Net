namespace HrDirector.DataBase.Model.Hackathon;

public class HackathonTeam
{
    public long Id { get; set; }
    public long HackathonId { get; set; }
    public HackathonEntity HackathonEntity { get; set; } = null!;
    public long TeamId { get; set; }
    public TeamEntity TeamEntity { get; set; } = null!;
}