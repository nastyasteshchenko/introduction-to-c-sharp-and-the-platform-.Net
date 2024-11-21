using System.ComponentModel.DataAnnotations.Schema;

namespace HrDirector.DataBase.Model.Employee;

[Table(name: "Employee")]
public class EmployeeEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Wishlist> Wishlists { get; set; } = null!;
    
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    
    public override bool Equals(object? obj)
    {
        if (obj == this)
        {
            return true;
        }

        if (obj is EmployeeEntity other)
        {
            return other.Id == Id;
        }

        return false;
    }
}