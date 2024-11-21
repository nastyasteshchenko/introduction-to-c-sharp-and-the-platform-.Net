using Nsu.Hackathon.Problem.HrDirector.DataBase.Model.Employee;

namespace Nsu.Hackathon.Problem.HrDirector.DataBase.Model.Hackathon;

public class HackathonParticipant
{
    public long Id { get; set; }
    public long HackathonId { get; set; }
    public HackathonEntity HackathonEntity { get; set; } = null!;
    public long ParticipantId { get; set; }
    public EmployeeEntity Participant { get; set; } = null!;
}