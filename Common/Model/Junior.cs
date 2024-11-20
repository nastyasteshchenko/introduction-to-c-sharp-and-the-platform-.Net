namespace Nsu.Hackathon.Problem.Common.Model;

public class Junior : Employee
{
    public override bool Equals(object? obj)
    {
        if (obj == this)
        {
            return true;
        }

        if (obj is Junior other)
        {
            return other.Id == Id;
        }

        return false;
    }
}