namespace Nsu.Hackathon.Problem.Common.Model;

public class TeamLead : Employee
{

    public override bool Equals(object? obj)
    {
        if (obj == this)
        {
            return true;
        }

        if (obj is TeamLead other)
        {
            return other.Id == Id;
        }

        return false;
    }
}