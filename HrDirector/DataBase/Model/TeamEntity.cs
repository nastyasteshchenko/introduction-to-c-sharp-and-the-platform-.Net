using System.ComponentModel.DataAnnotations.Schema;
using HrDirector.DataBase.Model.Employee;

namespace HrDirector.DataBase.Model;

[Table(name: "Team")]
public class TeamEntity
{
    public long Id { get; set; }
    public long TeamLeadId { get; set; }
    public EmployeeEntity TeamLead { get; set; } = null!;
    public long JuniorId { get; set; }
    public EmployeeEntity Junior { get; set; } = null!;
    
    public override string ToString()
    {
        return $"{TeamLead} - {Junior}";
    }

    public override bool Equals(object? obj)
    {
        if (obj == this)
        {
            return true;
        }
    
        if (obj is TeamEntity other)
        {
            return TeamLead.Equals(other.TeamLead) && Junior.Equals(other.Junior);
        }
    
        return false;
    }
}