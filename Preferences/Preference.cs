using Nsu.Hackathon.Problem.Worker;

namespace Nsu.Hackathon.Problem.Preferences;

public record Preference(Employee Employee, List<Employee> DesiredEmployees);