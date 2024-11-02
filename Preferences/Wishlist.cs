using Nsu.Hackathon.Problem.Worker;

namespace Nsu.Hackathon.Problem.Preferences;

public record Wishlist(EmployeeEntity Employee, List<EmployeeEntity> DesiredEmployees);