namespace HrDirector.DataBase.Model.Employee;

public class TeamLeadEntity : EmployeeEntity
{
    public override string ToString()
    {
        return $"TeamLead Id: {Id}, Name: {Name}";
    }
}