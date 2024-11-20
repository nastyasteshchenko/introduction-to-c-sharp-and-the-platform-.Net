namespace Nsu.Hackathon.Problem.Common.Model;

public class Team
{
    public long Id { get; set; }
    public long TeamLeadId { get; set; }
    public Employee TeamLead { get; set; } = null!;
    public long JuniorId { get; set; }
    public Employee Junior { get; set; } = null!;
    
    public override string ToString()
    {
        return $"{TeamLead} - {Junior}";
    }

    public override bool Equals(object? obj)
    {
        if (obj == this)
        {
            return true;
        }

        if (obj is Team other)
        {
            return TeamLead.Equals(other.TeamLead) && Junior.Equals(other.Junior);
        }

        return false;
    }
}