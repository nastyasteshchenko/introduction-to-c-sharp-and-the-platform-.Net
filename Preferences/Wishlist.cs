namespace Nsu.Hackathon.Problem.Preferences;

using Worker;

public class Wishlist
{
    public long Id { get; set; }
    public long PriorityNumber { get; set; } 
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public long DesiredEmployeeId { get; set; }
    public Employee DesiredEmployee { get; set; } = null!;
}