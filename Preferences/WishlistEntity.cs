using System.ComponentModel.DataAnnotations.Schema;

namespace Nsu.Hackathon.Problem.Preferences;

using Worker;

[Table("Wishlists")]
public class WishlistEntity
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; }
    public long DesiredEmployeeId { get; set; }
    public EmployeeEntity DesiredEmployee { get; set; }
}