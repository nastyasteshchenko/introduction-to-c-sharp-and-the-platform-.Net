using System.ComponentModel.DataAnnotations.Schema;
using Nsu.Hackathon.Problem.Preferences;

namespace Nsu.Hackathon.Problem.Worker;

[Table("Employees")]
public class EmployeeEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<WishlistEntity> Wishlists { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}";
    }
}