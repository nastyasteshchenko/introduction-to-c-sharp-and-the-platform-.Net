namespace Nsu.Hackathon.Problem.Common.Model;

public record TeamLead(long Id, string Name) : Employee(Id, Name);