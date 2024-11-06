using Nsu.Hackathon.Problem.Preferences;

namespace Nsu.Hackathon.Problem.Worker;

public class Employee
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Wishlist> Wishlists { get; set; } = null!;

    public override string ToString()
    {
        return $"{GetType().Name} Id: {Id}, Name: {Name} ";
    }

    public override bool Equals(object? obj)
    {
        if (obj == this)
        {
            return true;
        }

        if (obj is Employee other)
        {
            return other.Id == Id;
        }

        return false;
    }
}