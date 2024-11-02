namespace Nsu.Hackathon.Problem.TeamBuilding;

using Worker;

public class TeamEntity
{
    public long Id { get; set; }
    public EmployeeEntity TeamLead { get; set; }
    public EmployeeEntity Junior { get; set; }

    public override string ToString()
    {
        return $"TeamLead: ({TeamLead}) - Junior: ({Junior})";
    }
}