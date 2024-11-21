namespace HrDirector.DataBase.Model.Employee;

public class JuniorEntity : EmployeeEntity
{
    public override string ToString()
    {
        return $"Junior Id: {Id}, Name: {Name} Hash: {GetHashCode()}";
    }
}