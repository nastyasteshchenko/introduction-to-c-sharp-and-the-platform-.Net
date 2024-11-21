using Nsu.Hackathon.Problem.HrDirector.DataBase.Model.Employee;

namespace Nsu.Hackathon.Problem.HrDirector.DataBase.Model;

public class Wishlist
{
    public long Id { get; set; }
    public long PriorityNumber { get; set; } 
    public long EmployeeId { get; set; }
    public EmployeeEntity EmployeeEntity { get; set; } = null!;
    public long DesiredEmployeeId { get; set; }
    public EmployeeEntity DesiredEmployeeEntity { get; set; } = null!;
}